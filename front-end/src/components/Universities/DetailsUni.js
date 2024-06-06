
import React, { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import {
  Typography,
  Divider,
  Stack,
  IconButton,
  Container,
  Grid,
  Button,
  Dialog,
  DialogTitle,
  DialogActions,
  DialogContent,
  TextField,
  List
} from '@mui/material';
import { ArrowBack as ArrowBackIcon, ExpandMore as ExpandMoreIcon, Edit as EditIcon, Visibility as VisibilityIcon } from '@mui/icons-material';
import { motion } from 'framer-motion';
import { CloudUpload as CloudUploadIcon } from '@mui/icons-material';
import { Upload as UploadIcon } from '@mui/icons-material';


function DetailsUni() {
  const [university, setUniversity] = useState({});
  const { universityId } = useParams();
  const [loading, setLoading] = useState(true);
  const [animationComplete, setAnimationComplete] = useState(false);

  const [imageFile, setImageFile] = useState(null);
  const [editImageDialogOpen, setEditImageDialogOpen] = useState(false);




  useEffect(() => {
    if (universityId) {
      fetchUniversity();
    }
  }, [universityId]);

  useEffect(() => {
    setAnimationComplete(true);
  }, []);

  const fetchUniversity = async () => {
    try {
      const response = await fetch(`https://localhost:44364/api/University/GetUniversityById/${universityId}`);
      const data = await response.json();
      setUniversity(data);
      setLoading(false);
    } catch (error) {
      console.error('Error fetching university:', error);
      setLoading(false);
    }
  };

  const handleGoBack = () => {
    window.history.back();
  };

  const [expandedDescription, setExpandedDescription] = useState({});

  const toggleDescriptionExpansion = (universityId) => {
    setExpandedDescription((prevExpandedDescription) => ({
      ...prevExpandedDescription,
      [universityId]: !prevExpandedDescription[universityId],
    }));
  };


  const handleImageUpload = (event) => {
    const file = event.target.files[0];
    setImageFile(file);
  };

  const handleSaveImageUpload = async () => {
    try {
      const formData = new FormData();
      formData.append('file', imageFile);

      const response = await fetch(
        `https://localhost:44364/api/University/UploadProfilePhoto/${university.universityId}`,
        {
          method: 'POST',
          body: formData,
        }
      );
      if (response.ok) {
        setEditImageDialogOpen(false);
      } else {
        console.error('Error uploading image');
      }
    } catch (error) {
      console.error('Error during image upload:', error);
    }
  };

  const handleOpenEditImageDialog = () => {
    setEditImageDialogOpen(true);
  };

  const handleCloseEditImageDialog = () => {
    setEditImageDialogOpen(false);
  };





  return (
    <Container maxWidth="md" style={{ marginTop: '20px', padding: '20px' }}>
                    <motion.div
        initial={{ opacity: 0, y: 50 }}
        animate={animationComplete ? { opacity: 1, y: 0 } : {}}
        transition={{ duration: 0.7, delay: 0.2 }}
      >
      <Stack direction="row" alignItems="center" spacing={2} style={{ marginBottom: '20px' }}>
        <IconButton color="primary" onClick={handleGoBack}>
          <ArrowBackIcon />
        </IconButton>
        <Typography variant="h4">University Details</Typography>
      </Stack>
      <Divider />
      <Grid container spacing={2} marginTop={2}>
        <Grid item xs={12} md={6}>
          <IconButton
              onClick={() => setEditImageDialogOpen(true)}
              style={{ position: 'absolute' }}
            >
              {/* <CloudUploadIcon /> */}
              <UploadIcon />
            </IconButton>
          <img
              src={`https://localhost:44364/${university.profilePhotoPath}`}
              alt={university.name}
              style={{
                width: '300px',
                maxHeight: '300px',
                objectFit: 'cover',
                borderRadius: '8px',
              }}
            />

        </Grid>
        <Grid item xs={12} md={6}>
          <div style={{ padding: '20px' }}>
            <Typography variant="h6" style={{ marginBottom: '10px' }}>{university.name}</Typography>
            <Typography variant="body1" style={{ marginBottom: '10px' }}>
              <strong>Established Year:</strong> {university.establishedYear}
            </Typography>
            <Typography variant="body1" style={{ marginBottom: '10px' }}>
              <strong>Staff Number:</strong> {university.staffNumber}
            </Typography>
            <Typography variant="body1" style={{ marginBottom: '10px' }}>
              <strong>Student Number:</strong> {university.studentsNumber}
            </Typography>
            <Typography variant="body1" style={{ marginBottom: '10px' }}>
              <strong>Course Number:</strong> {university.coursesNumber}
            </Typography>

              <Typography variant="body1" style={{ marginBottom: '10px' }}>
              <strong>Description:</strong> {loading ? 'Loading...' : (
                expandedDescription[university.universityId]
                ? university.description
                : `${university.description.slice(0, 20)}...`
              )}
                  <Button
                    variant="text"
                    color="primary"
                    onClick={() => toggleDescriptionExpansion(university.universityId)}
                  >
                    {expandedDescription[university.universityId] ? 'See less' : 'See more'}
                  </Button>
            </Typography>

            <div style={{ display: 'flex', justifyContent: 'space-between', marginTop: '20px', maxWidth: '73%' }}>

            </div>
          </div>
        </Grid>

      </Grid>

      </motion.div>
      

      <Dialog open={editImageDialogOpen} onClose={handleCloseEditImageDialog}>
        <DialogTitle>Upload University Image</DialogTitle>
        <DialogContent>
          <input
            type="file"
            accept="image/*"
            onChange={handleImageUpload}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseEditImageDialog} color="primary">
            Cancel
          </Button>
          <Button onClick={handleSaveImageUpload} color="primary">
            Save
          </Button>
        </DialogActions>
      </Dialog>


    </Container>
  );
}

export default DetailsUni;
