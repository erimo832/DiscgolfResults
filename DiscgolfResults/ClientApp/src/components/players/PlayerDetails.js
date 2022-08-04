import 'bootstrap/dist/css/bootstrap.min.css';
import RefreshIcon from '@mui/icons-material/Refresh';
import FilterAltIcon from '@mui/icons-material/FilterAlt';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';
import { HoleAverage } from './HoleAverage';
import { Accordion, AccordionDetails, AccordionSummary, Box, Button, CircularProgress, Slider, Typography } from '@mui/material';
import { SummaryCard } from './SummaryCard';
import { ScoreDistribution } from './ScoreDistribution';
import { HcpTrend } from './HcpTrend';
import { AggregatedScoreTrend } from './AggregatedScoreTrend';
import { Events } from './Events';
import { HoleScoreDistributionPieChart } from '../common/HoleScoreDistributionPieChart';
import { Col, Container, Row } from 'react-bootstrap';


const style = {
  backgroundColor: "rgb(247, 247, 247)", 
  color: "rgb(102, 102, 102)"
};

export function PlayerDetails() {
  const handleChange = (event, value) => {
    if(value.length === 2) {
      setFromEvent(events[value[0] - 1].eventId);
      setToEvent(events[value[1] - 1].eventId);
    }

    setValue(value);
  };

  const [loading, setLoading] = useState(true);
  const [rounds, setRounds] = useState(null);

  //HoleAvg
  const [holeAvg, setHoleAvg] = useState(null);

  //Slider and events
  const [events, setEvents] = useState(null);
  const [fromEvent, setFromEvent] = useState(-1);
  const [toEvent, setToEvent] = useState(-1);
  const [value, setValue] = useState([1, 1]);

  let params = useParams(rounds);
  const { i18n } = useTranslation(); //t

  let contents = loading
      ? <Box display="flex" justifyContent="center" alignItems="center"><CircularProgress /></Box>
      : renderDetailsTable(rounds);

  useEffect(() => {
    if(rounds === null)
      fetchData(true);
  });


  function valueLabelFormat(value) {
    return events[value - 1].eventName + " (" + events[value - 1].startTime.substring(0,10) + ")";
  }

  function renderDetailsTable(info) {
    if(info === null)
        return (<div>{i18n.t('playersinfo_no_found')}</div>);
    
    let sortedEvents = info.eventResults.map( (x) => x ).sort( (a, b) => a.playedEvent - b.playedEvent ); //map for simple clone

    return (
        <>
          <Accordion disableGutters defaultExpanded={true} elevation={1}>
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
          <SummaryCard player={info} />
          <Accordion disableGutters>
            <AccordionSummary expandIcon={ <ExpandMoreIcon />} sx={style}>{i18n.t('player_details_holeavg')}</AccordionSummary>
            <AccordionDetails><HoleAverage data={holeAvg} /></AccordionDetails>
          </Accordion>
          <Accordion disableGutters>
            <AccordionSummary expandIcon={ <ExpandMoreIcon />} sx={style}>{i18n.t('player_details_holescoredistribution')}</AccordionSummary>
            <AccordionDetails>
              {holeAvg.map(x => <div style={{ display: "inline-block"}} key={x.courseHoleId}> <HoleScoreDistributionPieChart data={x} key={x.courseHoleId}/> </div> )}
            </AccordionDetails>
          </Accordion>
          <Accordion disableGutters>
            <AccordionSummary expandIcon={ <ExpandMoreIcon />} sx={style}>{i18n.t('player_details_hcptrend')}</AccordionSummary>
            <AccordionDetails><HcpTrend data={sortedEvents} /></AccordionDetails>
          </Accordion>
          <Accordion disableGutters>
            <AccordionSummary expandIcon={ <ExpandMoreIcon />} sx={style}>{i18n.t('player_details_scoretrend')}</AccordionSummary>
            <AccordionDetails><AggregatedScoreTrend data={sortedEvents} /></AccordionDetails>
          </Accordion>
          <Accordion disableGutters>
            <AccordionSummary expandIcon={ <ExpandMoreIcon />} sx={style}>{i18n.t('player_details_scoredistibution')}</AccordionSummary>
            <AccordionDetails><ScoreDistribution data={info.scoreDistibution} /></AccordionDetails>
          </Accordion>
          <Accordion disableGutters>
            <AccordionSummary expandIcon={ <ExpandMoreIcon />} sx={style}>{i18n.t('player_details_events')}</AccordionSummary>
            <AccordionDetails><Events data={info.eventResults} /></AccordionDetails>
          </Accordion>
        </>
    );
  }
  
  async function fetchData(isInit) {
    
    await fetchDetails();
    await fetchHoleAverages();

    if(isInit)
      await fetchEvents(isInit);

    setLoading(false);
  };

  async function fetchDetails() {
    let path = '/api/players/'+ params.playerId + '/details?fromEventId=' + fromEvent + '&toEventId=' + toEvent;
    const response = await fetch(path);
    const data = await response.json();

    setRounds(data);
  };

  async function fetchEvents(isInit) {
    let eventPath = '/api/players/'+ params.playerId + '/events';
    const responseEvents = await fetch(eventPath);
    const dataEvents = await responseEvents.json();

    if(isInit)
      setValue([1, dataEvents.length]);

    setEvents(dataEvents);
  };

  async function fetchHoleAverages() {
    let path = '/api/players/'+ params.playerId + '/average-hole-results?fromEventId=' + fromEvent + '&toEventId=' + toEvent;
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
