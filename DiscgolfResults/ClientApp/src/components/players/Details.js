import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { Grid } from '../common/Grid';
import { useParams } from 'react-router-dom';
import Collapse, { Panel } from 'rc-collapse';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import { HoleAverage } from './HoleAverage';
import { Box, CircularProgress } from '@mui/material';
import { PlayerStatistics } from './PlayerStatistics';
import { ScoreDistribution } from './ScoreDistribution';


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

    let hcpTrend = getHcpTrend(sortedEvents);
    let scoreTrend = getScoreTrend(sortedEvents);

    return (
        <>
          <PlayerStatistics player={info}></PlayerStatistics>
          <Collapse accordion={false}>
            <Panel header={i18n.t('player_details_hcptrend')}>
              {hcpTrend}
            </Panel>
            <Panel header={i18n.t('player_details_scoretrend')}>
              {scoreTrend}
            </Panel>
            <Panel header={i18n.t('player_details_holeavg')}>
              <HoleAverage playerId={params.playerId}></HoleAverage>
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
  
  function getHcpTrend(data) {    
    return (
      <ResponsiveContainer width="100%" height="100%" minHeight={300}>
        <LineChart
          width={500}
          height={300}
          data={data}
          margin={{ top: 5, right: 5, left: 5, bottom: 5, }}
        >
          <CartesianGrid strokeDasharray="3 3" />
          <XAxis dataKey="playedEvent" />
          <YAxis />
          <Tooltip content={<CustomTooltip />} />
          <Legend />
          <Line name={i18n.t('player_details_legend_hcp')} type="monotone" dataKey="hcpAfter" stroke="#8884d8" dot={false} />
        </LineChart>
      </ResponsiveContainer>
    );
  }

  function getScoreTrend(data) {
    let limit = 5;
    let cnt = 0;
    let sum = 0;
    let result = [];
    let from = data[0].startTime;    


    for (let i = 0; i < data.length; i++) {
        sum += data[i].score;
        cnt++;

        if(cnt === limit)
        {
            result.push(
              {
                avgScore: sum / cnt,
                numValues: cnt,
                from: from,
                to: data[i].startTime
              }
            );
            cnt = 0;
            sum = 0;
            from = data[i].startTime;
        }
    }

    //Add last
    if(cnt > 0) {
      result.push(
        {
          avgScore: sum / cnt,
          numValues: cnt,
          from: from,
          to: data[data.length - 1].startTime
        }
      );
    }

    return (
      <ResponsiveContainer width="100%" height="100%" minHeight={300}>
        <LineChart
          width={500}
          height={300}
          data={result}
          margin={{ top: 50, right: 5, left: 5, bottom: 5, }}
        >
          <CartesianGrid strokeDasharray="3 3" />
          <XAxis dataKey="playedEvent" />
          <YAxis domain={['auto', 'auto']} />
          <Tooltip content={<CustomAggregatedTooltip />} />
          <Legend />
          <Line name={i18n.t('player_details_legend_avgscore')} type="monotone" dataKey="avgScore" stroke="#8884d8" dot={false} />
        </LineChart>
      </ResponsiveContainer>
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

const CustomTooltip = ({ active, payload, label }) => {
  if (active && payload && payload.length) {
    return (
      <div className="custom-tooltip">        
          <p>{`Hcp: ${payload[0].value}`}</p>
          <p>
            {payload[0].payload.eventName} <br/>
            {payload[0].payload.startTime.substring(0,10)}
          </p>        
      </div>
    );
  }

  return null;
};

const CustomAggregatedTooltip = ({ active, payload, label }) => {
  const { i18n } = useTranslation();

  if (active && payload && payload.length) {
    return (
      <div className="custom-tooltip">        
          <p>{`${i18n.t('player_details_legend_avgscore')}: ${payload[0].value}`}</p>
          <p>
            {i18n.t('player_details_tooltip_numvalues')}: {payload[0].payload.numValues} <br/>
            {i18n.t('player_details_tooltip_between')}: {payload[0].payload.from.substring(0,10)} {i18n.t('player_details_tooltip_and')} {payload[0].payload.to.substring(0,10)}
          </p>        
      </div>
    );
  }

  return null;
};
