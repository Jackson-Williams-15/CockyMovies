import React from 'react';
import Diagnostics from './pages/Diagnostics';
import { makeStyles } from '@material-ui/core/styles';
import { BottomNavigation, BottomNavigationAction } from '@material-ui/core';
import RestoreIcon from '@material-ui/icons/Restore';
import FavoriteIcon from '@material-ui/icons/Favorite';
import LocationOnIcon from '@material-ui/icons/LocationOn';
import { Link } from 'react-router-dom';

const useStyles = makeStyles({
  root: {
    backgroundColor: 'silver',
    color: 'white',
  },
  selected: {
    color: 'yellow',
  },
});

export default function SimpleBottomNavigation() {
  const classes = useStyles();
  const [value, setValue] = React.useState(0);

  return (
    <BottomNavigation
      value={value}
      onChange={(event, newValue) => {
        setValue(newValue);
      }}
      showLabels
      className={classes.root}
    >
      <BottomNavigationAction label="Recents" icon={<RestoreIcon />} classes={{ selected: classes.selected }} />
      <BottomNavigationAction label="Diagnostics" icon={<FavoriteIcon />} component={Link} to="/diagnostics" classes={{ selected: classes.selected }} />
      <BottomNavigationAction label="Nearby" icon={<LocationOnIcon />} classes={{ selected: classes.selected }} />
    </BottomNavigation>
  );
}
