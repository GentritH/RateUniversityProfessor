
import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { Typography, Paper, Rating, Box, Avatar, Container, Button, Link, List, Input, Dialog,
  DialogContent,
  DialogTitle,
  Slide,
  AppBar,
  Toolbar,
  IconButton,
} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import { Directions } from '@mui/icons-material';
import SearchIcon from '@mui/icons-material/Search';
import Logo2 from '../logo/Logo2';
import ListRateProfessor from '../../components/ListRateProfessor'; 


function GetOverallRatingProfessor() {
  const { professorId } = useParams();
  const [overallRating, setOverallRating] = useState({});
  const [professor, setProfessor] = useState({});
  const [openDialog, setOpenDialog] = useState(false);
  
  useEffect(() => {
    fetchProfessorAndRating();
  }, []);

  const fetchProfessorAndRating = async () => {
    try {
      const professorResponse = await fetch(`https://localhost:44364/api/Professor/GetProfessorById/${professorId}`);
      const professorData = await professorResponse.json();
      setProfessor(professorData);

      const ratingResponse = await fetch(`https://localhost:44364/api/RateProfessor/GetOverallRatingForProfessors`);
      const ratingData = await ratingResponse.json();
      const professorRating = ratingData.find(rating => rating.professorId === parseInt(professorId));
      if (professorRating) {
        setOverallRating(professorRating);
      }
    } catch (error) {
      console.error('Error during request:', error);
    }
  };



  const handleOpenDialog = () => {
    setOpenDialog(true);
  };

  const handleCloseDialog = () => {
    setOpenDialog(false);
  };

  const Transition = React.forwardRef(function Transition(props, ref) {
    return <Slide direction="up" ref={ref} {...props} />;
  });

  return (
    <div>
                <section class="top-nav">
    <div>

    <Logo2 
                sx={{
                  position: 'relative',

                }}
    />

    </div>
    <input id="menu-toggle" type="checkbox" />
    <label class='menu-button-container' for="menu-toggle">
    <div class='menu-button'></div>
  </label>
    <ul class="menu">
      {/* <li>
      <Input
            className="searchProfList"
            type="text"
            placeholder="Search Professor..." 
            variant="outlined"
            // value={searchText}
            // onChange={(e) => setSearchText(e.target.value)}
            // onKeyPress={handleKeyPress} // Attach the event handler here
          />  
      </li> */}
      <li>
      <div className="sear">
      <Button /*onClick={handleSearch}*/><SearchIcon className="searIcon"  /></Button>
      <Input
        disableUnderline 
        className="searInput"
        type="text"
        placeholder="Search Professor..."
        //value={searchText}
        //onChange={(e) => setSearchText(e.target.value)}
      />
    </div>
      </li>
      <li>
      <List>
        <Link
                to="http://localhost:3000/universityList"
                className="button-47 "
                role="button"
                id="m"
              >
                Go to Universities
              </Link>
      </List>
      </li>

    </ul>
  </section>
      <Container sx={{marginTop: 10}}>
      {/* <Typography variant="h4">Professor Details and Ratings</Typography> */}
      <Paper elevation={3} style={{ display: 'flex', justifyContent: 'space-between', padding: '20px', width: '60%', margin: 'auto' }}>
        <Box style={{width: '50%'}} >
          <Avatar 
          alt={`${professor.firstName} ${professor.lastName}`} 
          // src="/path-to-avatar-image.jpg" 
          src={`https://localhost:44364/${professor.profilePhotoPath}`}
          sx={{ width: 150, height: 150, marginBottom: 2 }} />
          <Typography variant="h6">{professor.firstName} {professor.lastName}</Typography>
          <Typography>{professor.education}</Typography>
          <Typography sx={{marginBottom: 3}}>{professor.role}</Typography>
          <Link 

          to="#"
          onClick={handleOpenDialog}
          style={{ textDecoration: 'none' }}>
              <Button className='btn-mui-grayContained' variant="contained">
                See All Rates
              </Button>
            </Link>
        </Box>
        <Box style={{width: '50%'}}>

          <Box sx={{marginTop: 6}}>
            <Typography>
              <strong>Communication Skills:</strong>  <strong className='strRate'>{overallRating.communicationSkills}</strong> /5
            </Typography>
            {/* <Rating value={overallRating.communicationSkills} readOnly /> */}
          </Box>
          <Box>
            <Typography>
              <strong>Responsiveness:</strong> <strong className='strRate'>{overallRating.responsiveness}</strong> /5
            </Typography>
            {/* <Rating value={overallRating.responsiveness} readOnly /> */}
          </Box>
          <Box>
            <Typography>
              <strong>Grading Fairness:</strong> <strong className='strRate'>{overallRating.gradingFairness}</strong>/5
            </Typography>
            {/* <Rating value={overallRating.gradingFairness} readOnly /> */}
          </Box>
          <Typography sx={{marginTop: 11}}>
            Participants: {overallRating.totalRatings}
          </Typography>
        </Box>
      </Paper>
      </Container>
      <Dialog
        open={openDialog}
        onClose={handleCloseDialog}
        fullScreen
        maxWidth="md"
        TransitionComponent={Transition}
        fullWidth
      >
        <AppBar className='btn-mui-blackContained' sx={{ position: 'relative' }}>
          <Toolbar>
            <Button autoFocus color="inherit" onClick={handleCloseDialog}>
              Close
            </Button>
          </Toolbar>
        </AppBar>
        <DialogTitle>View All Rates</DialogTitle>
        <DialogContent>
          {/* Render the ListRateProfessor component in the dialog */}
          <ListRateProfessor />
        </DialogContent>
      </Dialog>
    </div>
  );
}

export default GetOverallRatingProfessor;
