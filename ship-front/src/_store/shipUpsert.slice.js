import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';

import { fetchWrapper } from '../_helpers';

// implementation
function createInitialState() {
  return {
    loading: true,
    done: false,
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
        const model = { id, name, width, length, shipCode };
        if (id) return await fetchWrapper.put(`${baseUrl}/${id}`, model);
        else return await fetchWrapper.post(baseUrl, model);
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
        state = { done: true };
      },
      [rejected]: (state, action) => {
        state = { error: 'error happened' };
      },
    };
  }
}

// create slice

const name = 'shipUpsert';
const initialState = createInitialState();
const extraActions = createExtraActions();
const extraReducers = createExtraReducers();
const slice = createSlice({ name, initialState, extraReducers });

// exports

export const shipUpsertActions = { ...slice.actions, ...extraActions };
export const shipUpsertReducer = slice.reducer;
