import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { Grid } from '../common/Grid';
import { useParams } from 'react-router-dom';
import Collapse, { Panel } from 'rc-collapse';
import { HoleAverage } from './HoleAverage';
import { Box, CircularProgress } from '@mui/material';
import { PlayerStatistics } from './PlayerStatistics';
import { ScoreDistribution } from './ScoreDistribution';
import { HcpTrend } from './HcpTrend';
import { AggregatedScoreTrend } from './AggregatedScoreTrend';


export function Details() {
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
          <PlayerStatistics player={info}></PlayerStatistics>
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
              <Grid data={info.eventResults} format={getGridConf()} />
            </Panel>
          </Collapse>
        </>
    );
  }

  function getGridConf()
  {
    return {
      className: "table table-striped",
      key: "eventId",
      detailsArray: "",
      detailsValue: "",
      rowClass: function(data) { 
        return data.inHcpAvgCalc ? 'avg' : (data.inHcpCalc === true ? 'top' : 'none' ); 
      },
      columns: [
        {columnName: "eventName",   headerText: i18n.t('column_round'),    headerClassName: "", columnClass: ""},
        {columnName: "startTime",   headerText: i18n.t('column_date'),     headerClassName: "", columnClass: "", formatedValue: function(x) { return x.startTime.substring(0,10); }},
        {columnName: "placementHcp",headerText: i18n.t('column_place'),    headerClassName: "", columnClass: ""},
        {columnName: "points",      headerText: i18n.t('column_points'),   headerClassName: "", columnClass: ""},
        {columnName: "score",       headerText: i18n.t('column_score'),    headerClassName: "", columnClass: ""},
        {columnName: "hcpBefore",   headerText: i18n.t('column_hcp'),      headerClassName: "d-none d-sm-table-cell", columnClass: "d-none d-sm-table-cell"},
        {columnName: "hcpScore",    headerText: i18n.t('column_hcpscore'), headerClassName: "d-none d-sm-table-cell", columnClass: "d-none d-sm-table-cell"}       
      ]
    };
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
