import Navbar from './components/Navbar';
import { ThemeProvider } from '@mui/material/styles';
import { Route, Routes, Link } from 'react-router-dom';
import Home from './components/pages/Home';
import About from './components/pages/About';
import Movies from './components/pages/Movies';
import Showtimes from './components/pages/Showtimes';
import Cart from './components/pages/Cart';
import Mail from './components/pages/Mail';
import Theme from './Theme';
import SignIn from './components/pages/SignIn';
import Signup from './components/pages/Signup';
import HomeFrame from './components/pages/HomeFrame';
import Tickets from './components/pages/Tickets';
import Profile from './components/pages/Profile';
import Manager from './components/pages/Manager';
import Shop from './components/pages/Shop';
import MovieReviews from './components/pages/MovieReviews';
import Music from './components/pages/Music';
import { AuthProvider } from './context/AuthContext';

function App() {
  return (
    <AuthProvider>
      <ThemeProvider theme={Theme}>
        <Navbar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/about" element={<About />} />
          <Route path="/homeframe" element={<HomeFrame />} />
          <Route path="/mail" element={<Mail />} />
          <Route path="/movies" element={<Movies />} />
          <Route path="/mail" element={<Mail />} />
          <Route path="/shop" element={<Shop />} />
          <Route path="/cart" element={<Cart />} />
          <Route path="/movies/:movieId/showtimes" element={<Showtimes />} />
          <Route path="/tickets/:showtimeId" element={<Tickets />} />
          <Route path="/login" element={<SignIn />} />
          <Route path="/music" element={<Music />} />
          <Route path="/signup" element={<Signup />} />
          <Route path="/profile/:username" element={<Profile />} />
          <Route path="/moviereviews" element={<MovieReviews />} />
          <Route path="/manager" element={<Manager />} />
        </Routes>
      </ThemeProvider>
    </AuthProvider>
  );
}

export default App;
