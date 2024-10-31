import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import { Link, useLocation } from 'react-router-dom';
import Box from '@mui/material/Box';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import { Button } from '@mui/material';
import { useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import LogoutIcon from '@mui/icons-material/Logout';
import './Navbar.css';

export default function Navbar() {
  const location = useLocation(); // Get the current path
  const { isAuthenticated, user, handleLogout } = useContext(AuthContext);

  return (
    <AppBar position="sticky" enableColorOnDark>
      <Toolbar>
        <Typography
          noWrap
          component={Link}
          to="/"
          sx={{
            mr: 2,
            fontFamily:
              'Impact, Haettenschweiler, Arial Narrow Bold, sans-serif',
            fontWeight: 700,
            color: 'inherit',
            textDecoration: 'none',
            fontSize: '1.7rem',
            cursor: 'pointer',
          }}
        >
          Cocky Movies
        </Typography>

        <Box sx={{ flexGrow: 1 }} />

        <Button
          component={Link}
          to="/"
          className={location.pathname === '/' ? 'MuiButton-root active' : ''}
          sx={{
            color: 'inherit',
            textDecoration: 'none',
            fontWeight: 500,
            marginRight: '20px',
          }}
        >
          Home
        </Button>
        <Button
          component={Link}
          to="/movies"
          className={
            location.pathname === '/movies' ? 'MuiButton-root active' : ''
          }
          sx={{
            color: 'inherit',
            textDecoration: 'none',
            fontWeight: 500,
            marginRight: '20px',
          }}
        >
          Movies
        </Button>

        {isAuthenticated ? (
          <>
            <Typography
              sx={{
                color: 'inherit',
                fontWeight: 500,
                marginRight: '10px',
              }}
            >
              Signed in as {user?.username}
            </Typography>
            <Button
              onClick={handleLogout}
              sx={{
                color: 'inherit',
                fontWeight: 500,
                marginLeft: '10px',
                marginRight: '20px',
              }}
            >
              <LogoutIcon />
            </Button>
          </>
        ) : (
          <>
            <Button
              component={Link}
              to="/login"
              sx={{ color: 'inherit', fontWeight: 500, marginRight: '20px' }}
              className={
                location.pathname === '/login' ? 'MuiButton-root active' : ''
              }
            >
              Login
            </Button>
            <Button
              component={Link}
              to="/signup"
              sx={{ color: 'inherit', fontWeight: 500, marginRight: '20px' }}
              className={
                location.pathname === '/signup' ? 'MuiButton-root active' : ''
              }
            >
              Signup
            </Button>
          </>
        )}

        <Button
          component={Link}
          to="/cart"
          sx={{ color: 'inherit', fontWeight: 500, marginLeft: '-25px' }}
        >
          <ShoppingCartIcon />
        </Button>
      </Toolbar>
    </AppBar>
  );
}
