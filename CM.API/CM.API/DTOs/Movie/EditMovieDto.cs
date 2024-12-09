// DTO used for editing an existing movie's details.
public class EditMovieDto
{
    // The ID of the movie that is being edited.
    public int OldMovieId { get; set; }

    // The new movie details that will replace the existing movie details.
    public MovieCreateDto NewMovie { get; set; }
}
