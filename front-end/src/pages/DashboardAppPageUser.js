
import { Helmet } from 'react-helmet-async';
import { useTheme } from '@mui/material/styles';
import { Grid, Container, Typography, Card, CardContent, CardMedia, Rating } from '@mui/material';
import React, { useState, useEffect } from 'react';

export default function DashboardAppPageUser() {
  const theme = useTheme();
  const [ratings, setRatings] = useState([]); 
  const [professors, setProfessors] = useState({}); 

  useEffect(() => {
    const studentEmail = localStorage.getItem('email');

    if (studentEmail) {
      fetch(`https://localhost:44364/api/User/GetUserByEmail/${studentEmail}`)
        .then(response => response.json())
        .then(data => {
          if (data && data.id) {
            fetchRatings(data.id); 
          }
        })
        .catch(error => {
          console.error('Error fetching studentId:', error);
        });
    }
  }, []);

  const fetchRatings = (studentId) => {
    fetch(`https://localhost:44364/api/RateProfessor/RateProfessors/Student/${studentId}`)
      .then(response => response.json())
      .then(data => {
        setRatings(data); 
        fetchProfessorData(data); 
      })
      .catch(error => {
        console.error('Error fetching professor ratings:', error);
      });
  };

  const fetchProfessorData = (ratings) => {
    const professorIds = ratings.map(rating => rating.professorId);
    professorIds.forEach(professorId => {
      fetch(`https://localhost:44364/api/Professor/GetProfessorById/${professorId}`)
        .then(response => response.json())
        .then(data => {
          setProfessors(prevProfessors => ({
            ...prevProfessors,
            [professorId]: data
          }));
        })
        .catch(error => {
          console.error(`Error fetching professor data for professor ${professorId}:`, error);
        });
    });
  };

  return (
    <>
      <Helmet>
        <title> Dashboard | Student </title>
      </Helmet>

      <Container maxWidth="xl">
        <Typography variant="h4" sx={{ mb: 5 }}>
          My professor ratings
        </Typography>

        <Grid container spacing={3}>
          {ratings.map(rating => (
            <Grid item key={rating.id} xs={12} sm={6} md={4}>
              <Card>
                <CardMedia
                  component="img"
                  height="140"
                  image={`https://localhost:44364/${professors[rating.professorId]?.profilePhotoPath}`}
                  alt={`Photo of ${professors[rating.professorId]?.firstName || 'Professor'}`}
                />
                <CardContent>
                  <Typography variant="h6">
                    {professors[rating.professorId]?.firstName} {professors[rating.professorId]?.lastName}
                  </Typography>
                  <Typography variant="body2">
                    Overall Rating: {rating.overall}
                  </Typography>
                  <Rating value={rating.overall} readOnly />
                  <Typography variant="body2">
                    Communication Skills: {rating.communicationSkills}
                  </Typography>
                  <Rating value={rating.communicationSkills} readOnly />
                  <Typography variant="body2">
                    Responsiveness: {rating.responsiveness}
                  </Typography>
                  <Rating value={rating.responsiveness} readOnly />
                  <Typography variant="body2">
                    Grading Fairness: {rating.gradingFairness}
                  </Typography>
                  <Rating value={rating.gradingFairness} readOnly />
                  <Typography variant="body2">
                    Feedback: {rating.feedback}
                  </Typography>
                </CardContent>
                
              </Card>
            </Grid>
          ))}
        </Grid>
      </Container>
    </>
  );
}
