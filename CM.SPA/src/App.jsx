import Navbar from './components/Navbar';
import { ThemeProvider } from '@mui/material/styles';
import { Route, Routes, Link } from 'react-router-dom';
import Home from './components/pages/Home';
import About from './components/pages/About';
import Movies from './components/pages/Movies';
import Showtimes from './components/pages/Showtimes';
import Checkout from './components/pages/Checkout';
import Cart from './components/pages/Cart';
import Theme from './Theme';
import SignIn from './components/pages/SignIn';
import Signup from './components/pages/Signup';
import Tickets from './components/pages/Tickets';
import { AuthProvider } from './context/AuthContext';

function App() {
  return (
    <AuthProvider>
      <ThemeProvider theme={Theme}>
        <Navbar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/about" element={<About />} />
          <Route path="/movies" element={<Movies />} />
          <Route path="/cart" element={<Cart />} />
          <Route path="/movies/:movieId/showtimes" element={<Showtimes />} />
          <Route path="/tickets/:showtimeId" element={<Tickets />} />
          <Route path="/login" element={<SignIn />} />
          <Route path="/signup" element={<Signup />} />
        </Routes>
      </ThemeProvider>
    </AuthProvider>
  );
}

export default App;
