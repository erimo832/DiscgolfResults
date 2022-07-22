import { Slider, Button, CircularProgress } from '@mui/material';
import RefreshIcon from '@mui/icons-material/Refresh';
import { Box } from '@mui/system';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

export function HoleAverage(props) {
  const { i18n } = useTranslation();

  const [loading, setLoading] = useState(true);  
  const [holeAvg, setHoleAvg] = useState(null);
  const [events, setEvents] = useState(null);

  ////////Slider
  const [fromEvent, setFromEvent] = useState(-1);
  const [toEvent, setToEvent] = useState(-1);
  const [value, setValue] = useState([1, 1]);


  const handleChange = (event, value) => {
    if(value.length === 2) {
      setFromEvent(events[value[0] - 1].eventId);
      setToEvent(events[value[1] - 1].eventId);
    }

    setValue(value);
  };

  function valueLabelFormat(value) {
    return events[value - 1].eventName + " (" + events[value - 1].startTime.substring(0,10) + ")";
  }

  /////////

  let contents = loading
      ? <Box display="flex" justifyContent="center" alignItems="center"><CircularProgress /></Box>
      : getContent(holeAvg);

  useEffect(() => {
    if(holeAvg === null)
      fetchData(true);
  });

  function getContent(data) {
    if(data.length === 0)
        return (<div>{i18n.t('playersinfo_no_found')}</div>);

    return (
      <>      
      <Button variant="outlined" startIcon={<RefreshIcon />} onClick={() => { fetchData(false); }}>
        {i18n.t('common_refresh')}
      </Button>
      <Box display="flex" flexDirection="column" ml={16} mr={16} mt={2} mb={2}>
        <Slider
          value={value}
          min={1}
          step={1}
          max={events.length}          
          getAriaValueText={valueLabelFormat}
          valueLabelFormat={valueLabelFormat}
          onChange={handleChange}
          valueLabelDisplay="auto"
          aria-labelledby="non-linear-slider"
        />
      </Box>
        <ResponsiveContainer width="100%" height="100%" minHeight={300}>
          <BarChart width={500} height={300} data={data} margin={{ top: 5, right: 5, left: 5, bottom: 5, }} >
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="holeNumber" />
            <YAxis domain={[2, 'auto']} />
            <Tooltip content={<HoleAvgTooltip />}  />
            <Legend />
            <Bar name={i18n.t('hole_stats_avgscore')} dataKey="averageScore" fill="#8884d8" />
            <Bar name={i18n.t('hole_stats_par')} dataKey="par" fill="#82ca9d" />
          </BarChart>
        </ResponsiveContainer>
        <ResponsiveContainer width="100%" height="100%" minHeight={300}>
          <BarChart width={500} height={300} data={data} margin={{ top: 5, right: 5, left: 5, bottom: 5, }} >
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="holeNumber" />
            <YAxis domain={['auto', 'auto']} />
            <Tooltip content={<HoleAvgTooltip />}  />
            <Legend />
            <Bar name={i18n.t('hole_stats_diffpar')} dataKey="diffToPar" fill="#8884d8" />
          </BarChart>
        </ResponsiveContainer>
      </>
    );
  }
  async function fetchData(isInit) {
    setLoading(true);

    let path = '/api/players/'+ props.playerId + '/average-hole-results?fromEventId=' + fromEvent + '&toEventId=' + toEvent;
    const response = await fetch(path);
    const data = await response.json();

    let eventPath = '/api/players/'+ props.playerId + '/events';
    const responseEvents = await fetch(eventPath);
    const dataEvents = await responseEvents.json();

    if(isInit)
      setValue([1, dataEvents.length]);

    setHoleAvg(data);
    setEvents(dataEvents);
    setLoading(false);
  };
  return (
    <div>
        {contents}
    </div>
  );
}

const HoleAvgTooltip = ({ active, payload, label }) => {
  const { i18n } = useTranslation();

  if (active && payload && payload.length) {
    let diff = payload[0].payload.averageScore - payload[0].payload.par;
    
    let diffLabel = diff < 0 ? i18n.t('hole_stats_diffpar') : i18n.t('hole_stats_diffpar');
    return (
      <div className="custom-tooltip">        
          <p>{`Hole: ${label}`}</p>
          <p>
            {diffLabel + ": " + payload[0].payload.diffToPar}<br/>
            {i18n.t('hole_stats_avgscore')}: {payload[0].payload.averageScore} ({i18n.t('hole_stats_par')}: {payload[0].payload.par})
            
          </p>        
      </div>
    );
  }

  return null;
};
