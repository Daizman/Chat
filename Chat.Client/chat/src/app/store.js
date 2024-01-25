import { configureStore } from "@reduxjs/toolkit";
import userReducer from "./Users/userSlice";


export default configureStore({
    reducer: {
        user: userReducer
    }
});
