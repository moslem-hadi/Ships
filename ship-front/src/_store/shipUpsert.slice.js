import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';

import { fetchWrapper } from '../_helpers';

// implementation

function createInitialState() {
  return {
    loading: true,
    done: false,
    error: null,
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
        state = { error: 'action.error' };
      },
    };
  }
}

// create slice

const name = 'shipUpsert1';
const initialState = createInitialState();
const extraActions = createExtraActions();
const extraReducers = createExtraReducers();
const slice = createSlice({ name, initialState, extraReducers });

// exports

export const shipUpsertActions = { ...slice.actions, ...extraActions };
export const shipUpsertReducer = slice.reducer;

// import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
// const baseUrl = `${process.env.REACT_APP_API_URL}ships`;

// export const upsert = createAsyncThunk(
//   'updateShips',
//   async (model, { dispatch, getState }) => {
//     return await fetch(`${baseUrl}/${model.id}`, {
//       method: 'POST', // *GET, POST, PUT, DELETE, etc.
//       mode: 'cors', // no-cors, *cors, same-origin
//       cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
//       credentials: 'same-origin', // include, *same-origin, omit
//       headers: {
//         'Content-Type': 'application/json',
//         // 'Content-Type': 'application/x-www-form-urlencoded',
//       },
//       redirect: 'follow', // manual, *follow, error
//       referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
//       body: JSON.stringify(model), // body data type must match "Content-Type" header
//     });
//   }
// );

// export const slice = createSlice({
//   name: 'updateShips',
//   initialState: {
//     loading: true,
//     done: false,
//     error: 'no error',
//   },
//   extraReducers: {
//     [upsert.pending]: (state, action) => {
//       state.loading = true;
//     },
//     [upsert.fulfilled]: (state, { payload }) => {
//       state.done = true;
//       state.loading = false;
//     },
//     [upsert.rejected]: (state, action) => {
//       state.error = 'failed';
//       state.loading = false;
//     },
//   },
// });

// export const shipUpsertActions = { ...slice.actions };
// export const shipUpsertReducer = slice.reducer;
