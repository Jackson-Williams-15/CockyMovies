import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import { Link, useLocation  } from 'react-router-dom';
import Box from '@mui/material/Box';
import './Navbar.css';
import { Button } from '@mui/material';

export default function Navbar() {
    const location = useLocation(); // Get the current path

    return (
      <AppBar position="sticky" enableColorOnDark>
        <Toolbar>
          <Typography
            noWrap
            component={Link}
            to="/"
            sx={{
              mr: 2,
              fontFamily: 'Impact, Haettenschweiler, Arial Narrow Bold, sans-serif',
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
            to="/about"
            className={location.pathname === '/about' ? 'MuiButton-root active' : ''}
            sx={{
              color: 'inherit',
              textDecoration: 'none',
              fontWeight: 500,
              marginRight: '20px',
            }}
          >
            About
          </Button>
        </Toolbar>
      </AppBar>
    );
}
