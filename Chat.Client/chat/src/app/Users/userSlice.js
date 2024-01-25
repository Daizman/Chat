import { createSlice } from "@reduxjs/toolkit";

const
    userSlice = createSlice({
        name: "user",
        initialState: {
            users: []
        },
        reducers: {
            addUser: (state, action) => {
                state.users.push(action.payload);
            },
            removeUserById: (state, action) => {
                state.users = state.users.filter(u => u.id !== action.payload);
            }
        }
    }),
    {
        addUser,
        removeUserById
    } = userSlice.actions,
    selectUsers = state => state.user.users;

export {
    addUser,
    removeUserById,
    selectUsers
};

export default userSlice.reducer;
