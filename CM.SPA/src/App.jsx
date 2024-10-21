import { useState } from 'react'
import Navbar from './components/Navbar';
import CssBaseline from '@mui/material/CssBaseline';
import Box from '@mui/material/Box';
import { ThemeProvider } from '@mui/material/styles';
import { Route, Routes, Link  } from 'react-router-dom';
import Home from './components/pages/Home';
import About from './components/pages/About';
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
      </Routes>
    </ThemeProvider>
    </div>
  );
}

export default App;