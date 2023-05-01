import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';

import { fetchWrapper } from '../_helpers';

// create slice

const name = 'shipUpsert';
const initialState = createInitialState();
const extraActions = createExtraActions();
const extraReducers = createExtraReducers();
const slice = createSlice({ name, initialState, extraReducers });

// exports

export const shipUpsertActions = { ...slice.actions, ...extraActions };
export const shipUpsertReducer = slice.reducer;

// implementation

function createInitialState() {
  return {
    loading: true,
  };
}

function createExtraActions() {
  const baseUrl = `${process.env.REACT_APP_API_URL}ships`;

  return {
    createShip: createShip(),
  };

  function createShip() {
    return createAsyncThunk(
      `${name}/post`,
      async ({ id, name, width, length, shipCode }, thunkApi) => {
        if (id)
          await fetchWrapper.put(`${baseUrl}/${id}`, {
            id,
            name,
            width,
            length,
            shipCode,
          });
        else
          await fetchWrapper.post(baseUrl, { name, width, length, shipCode });
      }
    );
  }
}

function createExtraReducers() {
  return {
    ...createShip(),
  };

  function createShip() {
    var { pending, fulfilled, rejected } = extraActions.createShip;
    return {
      [pending]: state => {
        state = { loading: true };
      },
      [fulfilled]: (state, action) => {
        state = { ...action.payload, added: true };
      },
      [rejected]: (state, action) => {
        state = { error: action.error };
      },
    };
  }
}
