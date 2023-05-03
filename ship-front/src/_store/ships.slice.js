import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';

import { fetchWrapper } from '../_helpers';

// create slice

const name = 'ships';
const initialState = createInitialState();
const extraActions = createExtraActions();
const extraReducers = createExtraReducers();
const slice = createSlice({ name, initialState, extraReducers });

// exports

export const shipsActions = { ...slice.actions, ...extraActions };
export const shipsReducer = slice.reducer;

// implementation

function createInitialState() {
  return {};
}

function createExtraActions() {
  const baseUrl = `${process.env.REACT_APP_API_URL}ships`;

  return {
    getAll: getAll(),
    deleteShip: deleteShip(),
  };

  function getAll() {
    const pageSize = 5;
    return createAsyncThunk(
      `${name}/getAll`,
      async ({ page }, thunkApi) =>
        await fetchWrapper.get(`${baseUrl}?page=${page}&pageSize=${pageSize}`)
    );
  }
  function deleteShip() {
    //I wont ask to delete! I can use sweetalert.
    return createAsyncThunk(
      `${name}/deleteShip`,
      async ({ shipId }, thunkApi) => {
        await fetchWrapper.delete(`${baseUrl}/${shipId}`);
        return thunkApi.dispatch(shipsActions.getAll({ page: 1 }));
      }
    );
  }
}

function createExtraReducers() {
  return {
    ...getAll(),
    ...deleteShip(),
  };

  function getAll() {
    var { pending, fulfilled, rejected } = extraActions.getAll;
    return {
      [pending]: state => {},
      [fulfilled]: (state, action) => {
        state.ships = action.payload;
      },
      [rejected]: (state, action) => {
        state.ships = { error: action.error };
      },
    };
  }
  function deleteShip() {
    var { pending, fulfilled, rejected } = extraActions.deleteShip;
    return {
      [pending]: state => {},
      [fulfilled]: (state, action) => {
        state.ships = { ...state.ships, deleted: true };
      },
      [rejected]: (state, action) => {
        state.ships = { ...state.ships, error: action.error };
      },
    };
  }
}
