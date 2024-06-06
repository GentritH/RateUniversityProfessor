
import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { Typography, Paper, Rating, Box, Avatar, Divider, Container, Button, Link, List, Input, Dialog,
  DialogContent,
  DialogTitle,
  Slide,
  AppBar,
  Toolbar,
  IconButton,
  Stack,
} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import { Directions } from '@mui/icons-material';
import SearchIcon from '@mui/icons-material/Search';
import Logo2 from '../logo/Logo2';
import ListRateProfessor from '../../components/ListRateProfessor'; 
import Iconify from '../iconify';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import CreateRateProfessor from '../../components/RateProfessor/CreateRateProfessor'; 




function DetProfessorUser() {
  const { professorId } = useParams();
  const [overallRating, setOverallRating] = useState({});
  const [professor, setProfessor] = useState({});
  const [openDialog, setOpenDialog] = useState(false);
  const [openCreateRateDialog, setOpenCreateRateDialog] = useState(false);


  const Transition = React.forwardRef(function Transition(props, ref) {
    return <Slide direction="up" ref={ref} {...props} />;
  });


  const [newRate, setNewRate] = useState({
    communicationSkills: 0,
    responsiveness: 0,
    gradingFairness: 0,
    feedback: "",
  });


  
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

  const handleGoBack = () => {
    window.history.back();
  };


  const handleRateChange = (field, value) => {
    setNewRate((prevRate) => ({
      ...prevRate,
      [field]: value,
    }));
  };

  const createRateForProfessor = async () => {
    try {
      const response = await fetch(
        `https://localhost:44364/api/RateProfessor/CreateRateProfessor`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            professorId: parseInt(professorId),
            ...newRate,
          }),
        }
      );
  
      if (response.ok) {
        // Do something after successfully creating the rate
        // For example, you could close the dialog and refresh the data
        handleCloseDialog();
        fetchProfessorAndRating();
      } else {
        console.error('Error creating rate:', response.statusText);
      }
    } catch (error) {
      console.error('Error during request:', error);
    }
  };
  


  const handleOpenCreateRateDialog = () => {
    setOpenCreateRateDialog(true);
  };
  const handleCloseCreateDialog = () => {
    setOpenCreateRateDialog(false);
  };
  

  return (
    <div>

              <div style={{ paddingLeft: '0px'}}>
        <Stack direction="row" alignItems="center" >
          <IconButton color="primary" onClick={handleGoBack}>
            <ArrowBackIcon />
          </IconButton>
          <Typography variant="h4">
            Create Rate
          </Typography>
        </Stack>
      </div>

      <Container sx={{marginTop: 0}}>
      {/* <Typography variant="h4">Professor Details and Ratings</Typography> */}
      <Paper elevation={3} style={{ display: 'flex', justifyContent: 'space-between', padding: '20px', width: '70%', margin: 'auto' }}>
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
          // to="http://localhost:3000/dashboard/createProfessor" 
          to="#"
          onClick={handleOpenDialog}
          style={{ textDecoration: 'none', marginRight: 10 }}>
              <Button variant="contained">
                See All Rates
              </Button>
            </Link>

                    <Link to="#" onClick={handleOpenCreateRateDialog} style={{ textDecoration: 'none' }}>
          <Button  variant="contained" startIcon={<Iconify icon="eva:plus-fill" />}>
            Create Rate
          </Button>

        </Link>


        </Box>
        <Divider orientation="vertical" flexItem />
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
            Participants test: {overallRating.totalRatings}
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
            {/* <IconButton
              edge="start"
              color="inherit"
              aria-label="close"
            >
              <CloseIcon />
            </IconButton>
            <Typography sx={{ ml: 2, flex: 1 }} variant="h6" component="div">
              Sound
            </Typography> */}
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


      <Dialog
        open={openCreateRateDialog}
        onClose={() => setOpenCreateRateDialog(false)}
        // fullScreen
        maxWidth="md"
        TransitionComponent={Transition}
        fullWidth
      >
                <AppBar className='btn-mui-grayContained' sx={{ position: 'relative' }}>
          <Toolbar>
            <IconButton
              edge="start"
              color="inherit"
              aria-label="close"
              onClick={handleCloseCreateDialog}
            >
              <CloseIcon />
            </IconButton>
            <Typography sx={{ ml: 2, flex: 1 }} variant="h6" component="div">
              Create rate for {professor.firstName} {professor.lastName}
            </Typography> 
            {/* <Button autoFocus color="inherit" onClick={handleCloseDialog}>
              Close
            </Button> */}
          </Toolbar>
        </AppBar>
        <DialogContent>
          <CreateRateProfessor professorId={professorId} />
        </DialogContent>
      </Dialog>


    </div>
  );
}

export default DetProfessorUser;