import 'bootstrap/dist/css/bootstrap.min.css';
import RefreshIcon from '@mui/icons-material/Refresh';
import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';
import Collapse, { Panel } from 'rc-collapse';
import { HoleAverage } from './HoleAverage';
import { Box, Button, CircularProgress, Slider } from '@mui/material';
import { SummaryCard } from './SummaryCard';
import { ScoreDistribution } from './ScoreDistribution';
import { HcpTrend } from './HcpTrend';
import { AggregatedScoreTrend } from './AggregatedScoreTrend';
import { Events } from './Events';


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
          
          <Button variant="outlined" startIcon={<RefreshIcon />} onClick={() => { fetchData(false); }}>
            {i18n.t('common_refresh')}
          </Button>
          <Box display="flex" flexDirection="column" ml={16} mr={16} mt={2} mb={2}>
            <Slider value={value} min={1} step={1} max={events.length} getAriaValueText={valueLabelFormat} valueLabelFormat={valueLabelFormat}
              onChange={handleChange} valueLabelDisplay="auto" aria-labelledby="non-linear-slider"/>
          </Box>
          <SummaryCard player={info} />
          <Collapse accordion={false}>
            <Panel header={i18n.t('player_details_hcptrend')}>
              <HcpTrend data={sortedEvents}></HcpTrend>
            </Panel>
            <Panel header={i18n.t('player_details_scoretrend')}>
              <AggregatedScoreTrend data={sortedEvents} />
            </Panel>
            <Panel header={i18n.t('player_details_holeavg')}>
              <HoleAverage data={holeAvg} />
            </Panel>
            <Panel header={i18n.t('player_details_scoredistibution')}>
              <ScoreDistribution data={info.scoreDistibution} />
            </Panel>
            <Panel header={i18n.t('player_details_events')}>
              <Events data={info.eventResults} />
            </Panel>
          </Collapse>
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
