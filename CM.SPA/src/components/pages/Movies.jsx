import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getMovies } from '../../Services/movieService';

export default function Movies() {
  const [movies, setMovies] = useState([]);

  useEffect(() => {
    const fetchMovies = async () => {
      try {
        const moviesData = await getMovies();
        setMovies(moviesData);
      } catch (error) {
        console.error('Failed to fetch movies:', error);
      }
    };

    fetchMovies();
  }, []);

  return (
    <div>
      <h2>Movies List</h2>
      <ul>
        {movies.map(movie => (
          <li key={movie.id}>
            <h3>
              <Link to={`/movies/${movie.id}/showtimes`}>{movie.title}</Link>
            </h3>
            <p>{movie.description}</p>
            <h4>
              {movie.genres && movie.genres.length > 0 ? (
                movie.genres.map(genre => genre.name).join(', ')
              ) : (
                'No genres available'
              )}
            </h4>
          </li>
        ))}
      </ul>
    </div>
  );
}