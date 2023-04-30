import { configureStore } from '@reduxjs/toolkit';

import { authReducer } from './auth.slice';
import { shipsReducer } from './ships.slice';

export * from './auth.slice';
export * from './ships.slice';

export const store = configureStore({
    reducer: {
        auth: authReducer,
        ships: shipsReducer
    },
});