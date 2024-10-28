import { createTheme } from '@mui/material/styles';

// Main color palatte for the application
const COLORS = {
  PRIMARY_MAIN: '#800000',
  SECONDARY_MAIN: '#ffffff',
  BACKGROUND_PAPER: '#eeeeee',
  BACKGROUND_DEFAULT: '#f3f6f4',
};

// Create Theme for the ThemeProvider
const Theme = createTheme({
  palette: {
    mode: 'light',
    primary: {
      main: COLORS.PRIMARY_MAIN,
    },
    secondary: {
      main: COLORS.SECONDARY_MAIN,
    },
    background: {
      paper: COLORS.BACKGROUND_PAPER,
      default: COLORS.BACKGROUND_DEFAULT,
    },
  },
  components: {
    MuiChip: {
      defaultProps: {
        variant: 'outlined',
      },
      styleOverrides: {
        root: {
          color: COLORS.PRIMARY_MAIN,
          borderColor: COLORS.PRIMARY_MAIN,
          border: '2px solid ${COLORS.PRIMARY_MAIN}',
        },
      },
    },
  },
});

export default Theme;
