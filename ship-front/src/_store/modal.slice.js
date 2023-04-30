import { createSlice } from '@reduxjs/toolkit';

// create slice
const name = 'modal';
const initialState = {
  isOpen: false,
};
const slice = createSlice({
  name,
  initialState,
  reducers: {
    openModal: (state, action) => {
      state.isOpen = true;
    },
    closeModal: (state, action) => {
      state.isOpen = false;
    },
  },
});

// exports

export const modalActions = { ...slice.actions };
export const modalReducers = slice.reducer;
