import React, { useEffect, useState } from 'react';
import axios from 'axios';

function Diagnostics() {
  const [data, setData] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios.get('http://class.usc547team4.info:8080/api/genre')
      .then(response => {
        setData(response.data);
      })
      .catch(error => {
        setError(error);
      });
  }, []);

  return (
    <div>
      <h1>Diagnostics Page [CE->GenreTest]:   API URL:http://class.usc547team4.info:8080/api/genre</h1>
      {error ? (
        <p>Error fetching diagnostics: {error.message}</p>
      ) : data ? (
        <div>
          <h2>Diagnostics Data:</h2>
          <pre>{JSON.stringify(data, null, 2)}</pre>
        </div>
      ) : (
        <p>Loading diagnostics data...</p>
      )}
    </div>
  );
}

export default Diagnostics;

