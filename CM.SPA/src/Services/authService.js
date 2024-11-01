import axios from 'axios';

const API_BASE_URL = '/api/Account';

export const login = async (credentials) => {
  try {
    const response = await axios.post(`${API_BASE_URL}/login`, credentials);
    // return token and store
    const { token } = response.data;
    localStorage.setItem('token', token); // Store the token in localStorage
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
    // returns token
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
};
