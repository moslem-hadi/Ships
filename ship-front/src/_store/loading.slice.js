import { createSlice } from '@reduxjs/toolkit';

// create slice
const name = 'loading';
const initialState = {
  loading: false,
};
const slice = createSlice({
  name,
  initialState,
  reducers: {
    startLoading: (state, action) => {
      state.loading = true;
    },
    stopLoading: (state, action) => {
      state.loading = false;
    },
  },
});

// exports

export const loadingActions = { ...slice.actions };
export const loadingReducers = slice.reducer;
