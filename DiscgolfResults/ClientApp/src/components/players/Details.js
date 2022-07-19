import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useState, useEffect, PureComponent } from 'react';
import i18n from "../../i18n";
import { Grid } from '../common/Grid';
import { useParams } from 'react-router-dom';
import Collapse, { Panel } from 'rc-collapse';
import {Container, Row, Col } from 'react-bootstrap'
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';



export function Details() {
  const [loading, setLoading] = useState(true);
  const [rounds, setRounds] = useState(null);
  let params = useParams(rounds);

  let contents = loading
      ? <p><em>{i18n.t('common_loading')}</em></p>
      : renderDetailsTable(rounds);

  useEffect(() => {
    if(rounds === null)
      fetchData();
  });

  function renderDetailsTable(info) {
    if(info.length === 0)
        return (<div>{i18n.t('playersinfo_no_found')}</div>);
    
    var stats = getStatTable(info);
    var hcpTrend = getHcpTrend(info);

    return (
        <>
          <h1>{info.fullName}</h1>          
          {stats}
          <Collapse accordion={true}>
            <Panel header="nt_Hcp trend">
              {hcpTrend}
            </Panel>
            <Panel header="nt_Events">
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

  function getStatTable(info)
  {
    return (
      <div>
          <div>nt_Total number of events: {info.totalRounds}</div>
          <div>nt_Total number of ctps: {info.totalCtps}</div>
          <div>nt_Ctp%: {info.ctpPercentage}%</div>
          <div>nt_First appearance : {info.firstAppearance.substring(0,10)}</div>
          <div>nt_Last appearance : {info.lastAppearance.substring(0,10)}</div>
          <div>nt_Best score: {info.bestScore}</div>
          <div>nt_Worst score: {info.worstScore}</div>
          <div>nt_Average score: {info.avgScore}</div>
      </div>
    );
  }

  function getHcpTrend(info) {
    var data = info.eventResults.sort( (a, b) => a.playedEvent - b.playedEvent );

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
          <Line name="nt_Handicap" type="monotone" dataKey="hcpAfter" stroke="#8884d8" dot={false} />
        </LineChart>
      </ResponsiveContainer>
    );
  }

  async function fetchData() {
    var path = '/api/players/'+ params.playerId + '/details';
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

