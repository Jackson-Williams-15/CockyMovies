<%@ Page Language="C#" AutoEventWireup="true" CodeFile="./code/Movie.aspx.cs" Inherits="Movies" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>CockyEntertainment -> Now Playing</title>
<style>
* {
  box-sizing: border-box;
}

blink {
                color: #2d38be;
                font-size: 22px;
                font-weight: bold;
            }

/* Create three equal columns that floats next to each other */
.column {
  float: left;
  width: 33.33%;
  padding: 10px;
  height: 460px; /* Should be removed. Only for demonstration */
}

/* Clear floats after the columns */
.row:after {
  content: "";
  display: table;
  clear: both;
}
</style>
</head>
<body>
<div align=center><span align=center><h1><font color=pink><blink>Now Playing</blink></font></h1></span></div>
    <form id="form1" runat="server">
    <div align="center">
	<span align=left><H3>Featured Movie: Marvel Comments presents XMen</H3></span>
	<span style="float:left;">&nbsp&nbsp&nbsp<asp:Image ID="jenimage" runat="server" height="60" width="60"/><BR></span>
        <span style="float:left;">&nbsp&nbsp&nbsp<asp:Label ID="Label1" runat="server" Text="Hello, MovieTimes!"></asp:Label></span><BR>
        <span style="float:left;">&nbsp&nbsp&nbsp<asp:Label ID="Label2" runat="server" Text="Hello, MovieDescription!"></asp:Label></span><BR>
        <span style="float:left;">&nbsp&nbsp&nbsp<asp:Label ID="Label3" runat="server" Text="Hello, OpenSeats"></asp:Label></span><BR>
    <iframe src="https://www.youtube.com/embed/SiO3vCBNt_A?si=rn0AvxFT8poPd7R0" width=100% height=350px></iframe><BR><BR>
    </div>
    </form>
    <div align=center>
<h3><font color=maroon>Action Movies</font></h3>
<div class="row">
  <div class="column" style="background-color:#aaa;">
    <div align=center><h3>James Bond - Casino Royale</h3></div>
    <iframe src="https://www.youtube.com/embed/d4ggLi3Us-A?si=R11k65ceLImgwlvo" width=100% height=300px></iframe>
    <span align=center><p>|&nbsp&nbsp&nbsp<a href="moviedescriptiondetail.aspx?id=1">Full Description</a>&nbsp&nbsp&nbsp | &nbsp&nbsp&nbsp<a href="https://react.usc547team4.info/shop/?product=adult-ticket-prime-time">Buy</a>&nbsp&nbsp&nbsp |&nbsp&nbsp&nbsp<a href="movietimesdetail.aspx?id=1">Showtimes</a>&nbsp&nbsp&nbsp|&nbsp&nbsp&nbsp<a href="moviereviewdetail.aspx?id=1">Reviews</a>&nbsp&nbsp&nbsp|</p></span>
  </div>
  <div class="column" style="background-color:#bbb;">
    <div align=center><h3>Tom Cruises - TopGun2</h3></div>

    <iframe src="https://www.youtube.com/embed/IXbnzEHZDPg?si=D_5tY7kQcYJTHY9Q" width=100% height=300px></iframe><span align=center><p>|&nbsp&nbsp&nbsp<a href="moviedescriptiondetail.aspx?id=2">Full Description</a>&nbsp&nbsp&nbsp | &nbsp&nbsp&nbsp<a href="https://react.usc547team4.info/shop/?product=sr-ticket-prime-time">Buy</a>&nbsp&nbsp&nbsp |&nbsp&nbsp&nbsp<a href="movietimesdetail.aspx?id=2"> Showtimes</a>&nbsp&nbsp&nbsp|&nbsp&nbsp&nbsp<a href="moviereviewdetail.aspx?id=2">Reviews</a>&nbsp&nbsp&nbsp|</p></span>

  </div>
  <div class="column" style="background-color:#ccc;">
    <div align=center><h3>Michele Rodriguez - Blue Crush </h3></div>
    <iframe src="https://www.youtube.com/embed/3q2PXB-aE7Q?si=L7gwg-5o7Y6GA2T9" width=100% height=300px></iframe><span align=center><p>|&nbsp&nbsp&nbsp<a href="moviedescriptiondetail.aspx?id=3">Full Description</a>&nbsp&nbsp&nbsp | &nbsp&nbsp&nbsp<a href="purchase.aspx?id=3">Buy</a>&nbsp&nbsp&nbsp |&nbsp&nbsp&nbsp<a href="movietimesdetail.aspx?id=3">Showtimes</a>&nbsp&nbsp&nbsp||&nbsp&nbsp&nbsp<a href="moviereviewdetail.aspx?id=3">Reviews</a>&nbsp&nbsp&nbsp|</p></span>

  </div>
</div>
<h3><font color=maroon>Romantic Comedies</font></h3>
<div class="row">
  <div class="column" style="background-color:#aaa;">
    <div align=center><h3>Shes All That</h3></div>

    <iframe src="https://www.youtube.com/embed/ExDPiPhLqEQ?si=u01ptHh-g_UdFxBX" width=100% height=300px></iframe><span align=center><p>|&nbsp&nbsp&nbsp<a href="moviedescriptiondetail.aspx?id=4">Full Description</a>&nbsp&nbsp&nbsp | &nbsp&nbsp&nbsp<a href="purchase.aspx?id=4">Buy</a>&nbsp&nbsp&nbsp |&nbsp&nbsp&nbsp<a href="movietimesdetail.aspx?id=4">Showtimes</a>&nbsp&nbsp&nbsp||&nbsp&nbsp&nbsp<a href="moviereviewdetail.aspx?id=4">Reviews</a>&nbsp&nbsp&nbsp|</p></span>

  </div>
  <div class="column" style="background-color:#bbb;">
    <div align=center><h3>Notting Hill</h3></div>

    <iframe src="https://www.youtube.com/embed/RESwG23_YGw?si=lv1oFVNzugrxgBwt" width=100% height=300px></iframe><span align=center><p>|&nbsp&nbsp&nbsp<a href="moviedescriptiondetail.aspx?id=5">Full Description</a>&nbsp&nbsp&nbsp | &nbsp&nbsp&nbsp<a href="purchase.aspx?id=5">Buy</a>&nbsp&nbsp&nbsp |&nbsp&nbsp&nbsp<a href="movietimesdetail.aspx?id=5">Showtimes</a>&nbsp&nbsp&nbsp||&nbsp&nbsp&nbsp<a href="moviereviewdetail.aspx?id=5">Reviews</a>&nbsp&nbsp&nbsp|</p></span>

  </div>
  <div class="column" style="background-color:#ccc;">
    <div align=center><h3>Jerry McGuire</h3></div>

    <iframe src="https://www.youtube.com/embed/FFrag8ll85w?si=ekjBdyw1D1vMMGTa" width=100% height=300px></iframe>
    <span align=center><p>|&nbsp&nbsp&nbsp<a href="moviedescriptiondetail.aspx">Full Description</a>&nbsp&nbsp&nbsp | &nbsp&nbsp&nbsp<a href="purchase.aspx?id=6">Buy</a>&nbsp&nbsp&nbsp |&nbsp&nbsp&nbsp<a href="showtimes.aspx?id=6">Showtimes</a>&nbsp&nbsp&nbsp||&nbsp&nbsp&nbsp<a href="moviereviewdetail.aspx?id=6">Reviews</a>&nbsp&nbsp&nbsp|</p></span>

  </div>

</div>
</body>
</html>