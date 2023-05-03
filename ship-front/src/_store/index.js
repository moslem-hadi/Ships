import { configureStore } from '@reduxjs/toolkit';

import { authReducer } from './auth.slice';
import { shipsReducer } from './ships.slice';
import { singleShipReducer } from './singleShip.slice';
import { shipUpsertReducer } from './shipUpsert.slice';
import { modalReducers } from './modal.slice';
import { loadingReducers } from './loading.slice';

export * from './auth.slice';
export * from './ships.slice';
export * from './singleShip.slice';
export * from './shipUpsert.slice';
export * from './modal.slice';
export * from './loading.slice';

export const store = configureStore({
  reducer: {
    auth: authReducer,
    ships: shipsReducer,
    modal: modalReducers,
    shipUpsert: shipUpsertReducer,
    ship: singleShipReducer,
    loading: loadingReducers,
  },
});
