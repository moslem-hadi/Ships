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
    return {
        ships: {}
    }
}

function createExtraActions() {
    const baseUrl = `${process.env.REACT_APP_API_URL}ships`;

    return {
        getAll: getAll()
    };    

    function getAll(page = 1, pageSize = 2) {
        debugger
        return createAsyncThunk(
            `${name}/`,
            async () => await fetchWrapper.get(`${baseUrl}?page=${page}&pageSize=${pageSize}`)
        );
    }
}

function createExtraReducers() {
    return {
        ...getAll()
    };

    function getAll(page = 1, pageSize = 2) {
        var { pending, fulfilled, rejected } = extraActions.getAll;
        return {
            [pending]: (state) => {
                state.ships = { loading: true };
            },
            [fulfilled]: (state, action) => {
                state.ships = action.payload;
            },
            [rejected]: (state, action) => {
                state.ships = { error: action.error };
            }
        };
    }
}
