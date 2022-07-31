import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';
import Collapse, { Panel } from 'rc-collapse';
import { HoleAverage } from './HoleAverage';
import { Box, CircularProgress } from '@mui/material';
import { SummaryCard } from './SummaryCard';
import { ScoreDistribution } from './ScoreDistribution';
import { HcpTrend } from './HcpTrend';
import { AggregatedScoreTrend } from './AggregatedScoreTrend';
import { Events } from './Events';


export function PlayerDetails() {
  const [loading, setLoading] = useState(true);
  const [rounds, setRounds] = useState(null);
  let params = useParams(rounds);
  const { i18n } = useTranslation(); //t

  let contents = loading
      ? <Box display="flex" justifyContent="center" alignItems="center"><CircularProgress /></Box>
      : renderDetailsTable(rounds);

  useEffect(() => {
    if(rounds === null)
      fetchData();
  });

  function renderDetailsTable(info) {
    if(info.length === 0)
        return (<div>{i18n.t('playersinfo_no_found')}</div>);
    
    let sortedEvents = info.eventResults.map( (x) => x ).sort( (a, b) => a.playedEvent - b.playedEvent ); //map for simple clone

    return (
        <>
          <SummaryCard player={info} />
          <Collapse accordion={false}>
            <Panel header={i18n.t('player_details_hcptrend')}>
              <HcpTrend data={sortedEvents}></HcpTrend>
            </Panel>
            <Panel header={i18n.t('player_details_scoretrend')}>
              <AggregatedScoreTrend data={sortedEvents} />
            </Panel>
            <Panel header={i18n.t('player_details_holeavg')}>
              <HoleAverage playerId={params.playerId} />
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
  
  async function fetchData() {
    let path = '/api/players/'+ params.playerId + '/details';
    const response = await fetch(path);
    const data = await response.json();

    setRounds(data);
    setLoading(false);
  };

  return (
    <div>
        {contents}
    </div>
  );
}
