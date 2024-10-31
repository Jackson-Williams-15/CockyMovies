import { useState } from 'react';
import Navbar from './components/Navbar';
import CssBaseline from '@mui/material/CssBaseline';
import Box from '@mui/material/Box';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import { ThemeProvider } from '@mui/material/styles';
import { Route, Routes, Link } from 'react-router-dom';
import Home from './components/pages/Home';
import About from './components/pages/About';
import Movies from './components/pages/Movies';
import Showtimes from './components/pages/Showtimes';
import Checkout from './components/pages/Checkout';
import Cart from './components/pages/Cart';
import './components/Navbar.css';
import Theme from './Theme';
function App() {
  return (
    <div>
      <ThemeProvider theme={Theme}>
        <Navbar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/about" element={<About />} />
          <Route path="/movies" element={<Movies />} />
          <Route path="/cart" element={<Cart />} />
          <Route path="/movies/:movieId/showtimes" element={<Showtimes />} />
          <Route path="/checkout" element={<Checkout  />} />
        </Routes>
      </ThemeProvider>
    </div>
  );
}

export default App;
