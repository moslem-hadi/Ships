import { store, authActions } from '../_store';

export const fetchWrapper = {
  get: request('GET'),
  post: request('POST'),
  put: request('PUT'),
  delete: request('DELETE'),
};

function request(method) {
  return (url, body) => {
    const requestOptions = {
      method,
      headers: authHeader(url),
    };
    if (body) {
      requestOptions.headers['Content-Type'] = 'application/json';
      requestOptions.body = JSON.stringify(body);
    }
    return fetch(url, requestOptions).then(handleResponse);
  };
}

// helper functions

function authHeader(url) {
  // return auth header with jwt if user is logged in and request is to the api url
  const token = authToken();
  const isLoggedIn = !!token;
  const isApiUrl = url.startsWith(process.env.REACT_APP_API_URL);
  if (isLoggedIn && isApiUrl) {
    return { Authorization: `Bearer ${token}` };
  } else {
    return {};
  }
}

function authToken() {
  return store.getState().auth.user?.token;
}

function handleResponse(response) {
  return response.text().then(text => {
    const data = text && JSON.parse(text);

    if (!response.ok) {
      if ([401, 403].includes(response.status) && authToken()) {
        // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
        const logout = () => store.dispatch(authActions.logout());
        logout();
      }
      let error = data && data?.title;
      if (data?.errors) error = flattenObj(data.errors)?.join(' ');
      return Promise.reject(error);
    }

    return data;
  });
}

function flattenObj(obj, parent, res = []) {
  for (let key in obj) {
    let propName = '';
    if (Array.isArray(obj)) propName = parent ? parent : key;
    else propName = parent ? parent + '_' + key : key;

    if (typeof obj[key] == 'object') {
      flattenObj(obj[key], propName, res);
    } else {
      res.push(obj[key]);
    }
  }
  return res;
}
