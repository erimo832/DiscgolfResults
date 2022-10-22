import 'rc-collapse/assets/index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import Collapse, { Panel } from 'rc-collapse';
import {Container, Row, Col } from 'react-bootstrap'
import i18n from "../../i18n";
import { Grid } from '../common/Grid';
import { Box, CircularProgress } from '@mui/material';

export class CtpLeaderboard extends Component {
  static displayName = CtpLeaderboard.name;

  constructor(props) {
    super(props);
    this.state = { series: [], loading: true };
  }

  componentDidMount() {
    this.populateResultData();
  }

  renderSeriesTable(series) {
    return (<div>           
      <Collapse
        accordion={false}
      >
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
                { series[i].divisionResults.map(x => 
                  <Collapse accordion={false} key={series[i].serieId + x.division + i}>
                    <Panel header={`${x.division}`} key={series[i].serieId + x.division}>
                      <Grid data={this.getDataForGrid(x.ctpResults)} format={this.getGridConf()} />
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
      detailsArray: "",
      detailsValue: "",
      columns: [
        {columnName: "placement",       headerText: i18n.t('column_place'),         headerClassName: "", columnClass: ""},
        {columnName: "fullName",        headerText: i18n.t('column_name'),          headerClassName: "", columnClass: ""},
        {columnName: "numberOfCtps",    headerText: i18n.t('column_numerofctps'),   headerClassName: "", columnClass: ""},      
        {columnName: "numberOfEvents",  headerText: i18n.t('column_rounds'),        headerClassName: "d-none d-sm-table-cell", columnClass: "d-none d-sm-table-cell"}        
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
        <h1 id="tabelLabel">{i18n.t('leaderboard_ctp_header')}</h1>
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