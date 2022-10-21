import 'rc-collapse/assets/index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import Collapse, { Panel } from 'rc-collapse';
import {Container, Row, Col } from 'react-bootstrap'
import i18n from "../../i18n";
import { Grid } from '../common/Grid';
import { Box, CircularProgress } from '@mui/material';

export class PointLeaderboard extends Component {
  static displayName = PointLeaderboard.name;
  
  constructor(props) {
    super(props);
    this.state = 
    { 
      series: [], 
      loading: true,
      selectedPlayers: [],
      sortColumn: 'place',
      sortOrder: 'desc'
    };
  }

  componentDidMount() {
    this.populateResultData();
  }

  renderSeriesTable(series) {
    return (<div>
      <Collapse
        accordion={false}>
        {this.getItems(series)}
      </Collapse>
    </div>
    );
  }

  getItems(series) {
    const items = [];
    
    for (let i = 0, len = series.length; i < len; i++) {
      items.push(
        <Panel header={`${series[i].serieName}`} key={series[i].serieId}>
          <Container>
            <Row>
               <Col sm={12} lg={12}>
                   <div>{i18n.t('series_basednumrounds', {cnt: series[i].roundsToCount } )}</div>
               </Col>
            </Row>
            <Row>
              <Col sm={12} lg={12}>
                { series[i].divisionResults.map(x => 
                  <Collapse accordion={false} key={series[i].serieId + x.division + i}>
                    <Panel header={`${x.division}`} key={series[i].serieId + x.division}>
                      <Grid data={this.getDataForGrid(x.hcpResults)} format={this.getGridConf()} />
                    </Panel>
                  </Collapse>) 
                }
              </Col>
            </Row>
        </Container>
        </Panel>
      );
    }
    return items;
  }

  getGridConf()
  {
    return {
      className: "table",
      key: "playerId",
      detailsArray: "eventPoints",
      detailsValue: "points",
      columns: [
        {columnName: "placement",       headerText: i18n.t('column_place'),         headerClassName: "", columnClass: ""},
        {columnName: "fullName",        headerText: i18n.t('column_name'),          headerClassName: "", columnClass: ""},
        {columnName: "totalPoints",     headerText: i18n.t('column_totalpoints'),   headerClassName: "", columnClass: ""},
        {columnName: "avgPoints",       headerText: i18n.t('column_avgpoints'),     headerClassName: "d-none d-lg-table-cell", columnClass: "d-none d-lg-table-cell"},
        {columnName: "avgHcpScore",     headerText: i18n.t('column_avghcpscore'),   headerClassName: "d-none d-md-table-cell", columnClass: "d-none d-md-table-cell"},
        {columnName: "totalHcpScore",   headerText: i18n.t('column_totalhcpscore'), headerClassName: "d-none d-lg-table-cell", columnClass: "d-none d-lg-table-cell"},
        {columnName: "maxPoints",       headerText: i18n.t('column_maxpoints'),     headerClassName: "d-none d-sm-table-cell", columnClass: "d-none d-sm-table-cell"},
        {columnName: "minPoints",       headerText: i18n.t('column_minpoints'),     headerClassName: "d-none d-sm-table-cell", columnClass: "d-none d-sm-table-cell"},        
        {columnName: "numberOfEvents",  headerText: i18n.t('column_rounds'),        headerClassName: "d-none d-lg-table-cell", columnClass: "d-none d-lg-table-cell"}        
      ]
    };
  }

  getDataForGrid(data)
  {
    return data;
  }

  render() {
    let contents = this.state.loading
      ? <Box display="flex" justifyContent="center" alignItems="center"><CircularProgress /></Box>
      : this.renderSeriesTable(this.state.series);
     
    return (
      <div>
        <h1 id="tabelLabel">{i18n.t('leaderboard_points_header')}</h1>
        <p>{i18n.t('series_description')}</p>
        {contents}
      </div>
    );
  }

  async populateResultData() {
    const response = await fetch('api/series/leaderboards');
    const data = await response.json();
    this.setState({ series: data, loading: false });
  }
}
