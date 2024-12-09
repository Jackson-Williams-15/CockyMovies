import Iframe from 'react-iframe';
import React from 'react';
import './HomeFrame.css';

const HomeFrame = () => {
  return (
  		<>  
  <div>
  		<span><iframe src="http://www.abcnews.com" width="49%" height="1200px"/></span>&nbsp&nbsp<span><iframe src="http://www.disney.com"width="49%" height="1200px"/></span>
  </div>
</>
     
  );
};


export default HomeFrame;
