import { createSlice } from "@reduxjs/toolkit";
import { getUser } from "../Identity/authService";

const
    userSlice = createSlice({
        name: "user",
        initialState: {
            token: null,
            curUser: null,
            isLogged: false,
        },
        reducers: {
            setToken: (state, action) => {
                state.token = action.payload;
            },
            setUser: (state, action) => {
                state.curUser = action.payload;
            },
            setLogged: (state, action) => {
                state.isLogged = action.payload;
            },
        }
    }),
    {
        setToken,
        setUser,
        setLogged,
    } = userSlice.actions,
    reset = () => (dispatch) => {
        console.log("reset");
        dispatch(setUser(null));
        dispatch(setLogged(false));
        dispatch(setToken(null));
    },
    loadUser = () => (dispatch) => {
        getUser().then(user => {
            if (user && !user.expired) {
                dispatch(setUser(user));
                dispatch(setLogged(true));
                dispatch(setToken(user.access_token));
            } else {
                dispatch(reset());
            }
        }).catch(err => {
            console.error(err);
            dispatch(reset());
        });
    },

    selectToken = state => state.user.token,
    selectUser = state => state.user.curUser,
    selectLogged = state => state.user.isLogged;

export {
    setToken,
    setUser,
    setLogged,
    loadUser,
    selectToken,
    selectUser,
    selectLogged,
};

export default userSlice.reducer;
