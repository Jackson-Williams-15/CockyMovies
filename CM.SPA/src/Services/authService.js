import axios from 'axios';

const API_BASE_URL = '/api/Account';

export const login = async (credentials) => {
  try {
    const response = await axios.post(`${API_BASE_URL}/login`, credentials);
    // return token, cartId, user and store
    const { token, cartId, user } = response.data;
    localStorage.setItem('token', token); // Store the token in localStorage
    localStorage.setItem('cartId', cartId); // Store the cartId in localStorage
    localStorage.setItem('user', JSON.stringify(user)); // Store the user in localStorage
    return response.data;
  } catch (error) {
    console.error(
      'Login error:',
      error.response ? error.response.data : error.message,
    );
    throw error;
  }
};

export const signup = async (credentials) => {
  try {
    const response = await axios.post(`${API_BASE_URL}/signup`, credentials);
    // returns token, cart, and user
    const { token, cartId, user } = response.data;
    localStorage.setItem('token', token); // Store the token in localStorage
    localStorage.setItem('cartId', cartId); // Store the cartId in localStorage
    localStorage.setItem('user', JSON.stringify(user)); // Store the user in localStorage
    return response.data;
  } catch (error) {
    console.error(
      'Signup error:',
      error.response ? error.response.data : error.message,
    );
    throw error;
  }
};

export const logout = () => {
  localStorage.removeItem('token');
  localStorage.removeItem('cartId');
  localStorage.removeItem('user');
};
