import React from 'react';
import { Box, Grid, TextField, Button, FormControl, InputLabel, Select, MenuItem } from '@mui/material';

const EditShowtimeForm = ({ editShowtimeData, handleEditShowtimeChange, handleEditShowtimeSubmit, showtimes }) => {
  return (
    <Box component="form" onSubmit={handleEditShowtimeSubmit} sx={{ mt: 4, width: '100%', maxWidth: 600 }}>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <FormControl fullWidth required>
            <InputLabel>Showtime</InputLabel>
            <Select
              label="Showtime"
              name="id"
              value={editShowtimeData.id}
              onChange={handleEditShowtimeChange}
            >
              <MenuItem value="">
                <em>None</em>
              </MenuItem>
              {showtimes.map((showtime) => (
                <MenuItem key={showtime.id} value={showtime.id}>
                  {new Date(showtime.startTime).toLocaleString('en-US', {
                    year: 'numeric',
                    month: 'long',
                    day: 'numeric',
                    hour: 'numeric',
                    minute: 'numeric',
                    hour12: true,
                  })}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <TextField
            label="Start Time"
            name="startTime"
            type="datetime-local"
            value={editShowtimeData.startTime}
            onChange={handleEditShowtimeChange}
            fullWidth
            InputLabelProps={{
              shrink: true,
            }}
            required
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            label="Capacity"
            name="capacity"
            type="number"
            value={editShowtimeData.capacity}
            onChange={handleEditShowtimeChange}
            fullWidth
            required
          />
        </Grid>
        <Grid item xs={12}>
          <Button type="submit" variant="contained" color="primary" fullWidth>
            Edit Showtime
          </Button>
        </Grid>
      </Grid>
    </Box>
  );
};

export default EditShowtimeForm;