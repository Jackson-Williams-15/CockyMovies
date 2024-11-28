import React, { createContext, useState, useEffect } from 'react';
import { login, signup, logout } from '../Services/authService';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState(null);

  useEffect(() => {
    const token = localStorage.getItem('token');
    const userData = localStorage.getItem('user');
    if (token && userData) {
      setIsAuthenticated(true);
      setUser(JSON.parse(userData));
    }
  }, []);

  // handles logins
  const handleLogin = async (credentials) => {
    try {
      const data = await login(credentials);
      if (data.token) {
        localStorage.setItem('token', data.token);
        localStorage.setItem('cartId', data.cartId);
        localStorage.setItem('user', JSON.stringify(data.user));
        localStorage.setItem('userId', data.user.id);
        setIsAuthenticated(true);
        setUser(data.user);
      } else {
        console.error('AuthContext - No token received');
      }
    } catch (error) {
      console.error('AuthContext - Login error:', error);
    }
  };

  // handles sign ups
  const handleSignup = async (credentials) => {
    try {
      const data = await signup(credentials);
      if (data.token) {
        localStorage.setItem('token', data.token);
        localStorage.setItem('cartId', data.cartId);
        localStorage.setItem('user', JSON.stringify(data.user));
        localStorage.setItem('userId', data.user.id);
        setIsAuthenticated(true);
        setUser(data.user);
      } else {
        console.error('AuthContext - No token received');
      }
    } catch (error) {
      console.error('AuthContext - Signup error:', error);
    }
  };

  // handles log outs
  const handleLogout = () => {
    logout();
    setIsAuthenticated(false);
    setUser(null);
  };

  const updateUser = (updatedUser) => {
    localStorage.setItem('user', JSON.stringify(updatedUser));
    setUser(updatedUser);
  };

  return (
    <AuthContext.Provider
      value={{
        isLoggedIn: isAuthenticated,
        username: user?.username,
        handleLogin,
        handleSignup,
        handleLogout,
        updateUser,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};