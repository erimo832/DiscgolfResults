import 'bootstrap/dist/css/bootstrap.min.css';
import RefreshIcon from '@mui/icons-material/Refresh';
import FilterAltIcon from '@mui/icons-material/FilterAlt';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';
import { Accordion, AccordionDetails, AccordionSummary, Box, Button, CircularProgress, Slider, Typography } from '@mui/material';
import { Col, Container, Row } from 'react-bootstrap';
import { HoleAverage } from '../players/HoleAverage';
import { CourseCard } from './CourseCard';
import { HoleScoreDistributionPieChart } from '../common/HoleScoreDistributionPieChart';
import { ScoreDistribution } from '../players/ScoreDistribution';


const style = {
  backgroundColor: "rgb(247, 247, 247)", 
  color: "rgb(102, 102, 102)"
};

export function CourseDetails() {
  const handleChange = (event, value) => {
    if(value.length === 2) {
      setFromEvent(events[value[0] - 1].eventId);
      setToEvent(events[value[1] - 1].eventId);
    }

    setValue(value);
  };

  const [loading, setLoading] = useState(true);
  const [courseDetails, setCourseDetails] = useState(null);

  //HoleAvg
  const [holeAvg, setHoleAvg] = useState(null);

  //Slider and events
  const [events, setEvents] = useState(null);
  const [fromEvent, setFromEvent] = useState(-1);
  const [toEvent, setToEvent] = useState(-1);
  const [value, setValue] = useState([1, 1]);

  let params = useParams();
  const { i18n } = useTranslation(); //t

  let contents = loading
      ? <Box display="flex" justifyContent="center" alignItems="center"><CircularProgress /></Box>
      : renderDetailsTable();

  useEffect(() => {
    if(courseDetails === null)
      fetchData(true);
  });


  function valueLabelFormat(value) {
    return events[value - 1].eventName + " (" + events[value - 1].startTime.substring(0,10) + ")";
  }

  function renderDetailsTable() {
    return (
        <>
          <Accordion disableGutters defaultExpanded={false} elevation={1}>
            <AccordionSummary expandIcon={ <ExpandMoreIcon />} sx={style}>
              <Typography><FilterAltIcon /> {i18n.t('common_filter')}</Typography>
            </AccordionSummary>
            <AccordionDetails>
              <Container>
                <Row>
                  <Col style={{textAlign: 'right'}}>
                    <Button variant="outlined" startIcon={<RefreshIcon />} onClick={() => { fetchData(false); }}>
                      {i18n.t('common_refresh')}
                    </Button>
                  </Col>
                </Row>
              </Container>
              <Box display="flex" flexDirection="column" ml={6} mr={6} mt={1} mb={1}>
                <Slider value={value} min={1} step={1} max={events.length} getAriaValueText={valueLabelFormat} valueLabelFormat={valueLabelFormat}
                  onChange={handleChange} valueLabelDisplay="auto" aria-labelledby="non-linear-slider"/>
              </Box>
            </AccordionDetails>
          </Accordion>
          <CourseCard course={courseDetails} />
          {holeAvg.map(x => 
            <Accordion key={x.courseLayoutId} disableGutters>
              <AccordionSummary expandIcon={ <ExpandMoreIcon />} sx={style}>{i18n.t('common_course_layout')} ({x.layoutName})</AccordionSummary>
              <AccordionDetails>                
                <Accordion disableGutters>
                  <AccordionSummary expandIcon={ <ExpandMoreIcon />} sx={style}>{i18n.t('player_details_holescoredistribution')}</AccordionSummary>
                  <AccordionDetails>
                    {x.holeResults.map(y => <div style={{ display: "inline-block"}} key={y.courseHoleId}> <HoleScoreDistributionPieChart data={y} key={y.courseHoleId}/> </div> )}
                  </AccordionDetails>
                </Accordion>
                <Accordion disableGutters>
                  <AccordionSummary expandIcon={ <ExpandMoreIcon />} sx={style}>{i18n.t('player_details_holeavg')}</AccordionSummary>
                  <AccordionDetails><HoleAverage data={x.holeResults} /></AccordionDetails>
                </Accordion>
              </AccordionDetails>              
            </Accordion> 
          )}
          <Accordion disableGutters>
            <AccordionSummary expandIcon={ <ExpandMoreIcon />} sx={style}>{i18n.t('player_details_scoredistibution')}</AccordionSummary>
            <AccordionDetails><ScoreDistribution data={courseDetails.scoreDistibution} /></AccordionDetails>
          </Accordion>
        </>
    );
  }
  
  async function fetchData(isInit) {
    
    await fetchDetails(isInit);
    await fetchHoleAverages();

    setLoading(false);
  };

  async function fetchDetails(isInit) {
    let path = '/api/courses/'+ params.courseId + '/details?fromEventId=' + fromEvent + '&toEventId=' + toEvent;
    const response = await fetch(path);
    const data = await response.json();

    setCourseDetails(data);

    if(isInit) {
        setEvents(data.events);
        setValue([1, data.events.length]);      
    }
  };

  async function fetchHoleAverages() {
    let path = '/api/courses/'+ params.courseId + '/average-hole-results?fromEventId=' + fromEvent + '&toEventId=' + toEvent;
    const response = await fetch(path);
    const data = await response.json();

    setHoleAvg(data);
  };

  return (
    <div>
        {contents}
    </div>
  );
}
