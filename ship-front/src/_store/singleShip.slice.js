import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';

import { fetchWrapper } from '../_helpers';

// create slice

const name = 'singleShip';
const initialState = createInitialState();
const extraActions = createExtraActions();
const extraReducers = createExtraReducers();
const slice = createSlice({ name, initialState, extraReducers });

// exports

export const singleShipActions = { ...slice.actions, ...extraActions };
export const singleShipReducer = slice.reducer;

// implementation

function createInitialState() {
  return {
    ship: {
      loading: true,
    },
  };
}

function createExtraActions() {
  const baseUrl = `${process.env.REACT_APP_API_URL}ships`;

  return {
    GetById: GetById(),
  };

  function GetById() {
    return createAsyncThunk(
      `${name}/GetById`,
      async ({ shipId }, thunkApi) =>
        await fetchWrapper.get(`${baseUrl}/${shipId}`)
    );
  }
}

function createExtraReducers() {
  return {
    ...GetById(),
  };

  function GetById() {
    var { pending, fulfilled, rejected } = extraActions.GetById;
    return {
      [pending]: state => {
        state.ship = { loading: true };
      },
      [fulfilled]: (state, action) => {
        state.ship = action.payload;
      },
      [rejected]: (state, action) => {
        state.ship = { error: action.error };
      },
    };
  }
}
