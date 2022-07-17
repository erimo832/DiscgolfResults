import 'rc-collapse/assets/index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import Collapse, { Panel } from 'rc-collapse';
import {Container, Row, Col } from 'react-bootstrap'
import i18n from "../../i18n";
import { Grid } from '../common/Grid';

export class ScoreLeaderboard extends Component {
  static displayName = ScoreLeaderboard.name;

  constructor(props) {
    super(props);
    this.state = 
    { 
      series: [], 
      loading: true,
      selectedPlayers: [] 
    };
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
                   <div>{i18n.t('series_basednumrounds', {cnt: series[i].roundsToCount } )}</div>
               </Col>
            </Row>
            <Row>
              <Col sm={12} lg={12}>
                <Grid data={this.getDataForGrid(series[i].scoreResults)} format={this.getGridConf()} />
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
      detailsArray: "eventScores",
      detailsValue: "score",
      columns: [
        {columnName: "placement",       headerText: i18n.t('column_place'),     headerClassName: "",                        rowClassName: ""},
        {columnName: "fullName",        headerText: i18n.t('column_name'),      headerClassName: "",                        rowClassName: ""},
        {columnName: "avgScore",        headerText: i18n.t('column_avgscore'),  headerClassName: "",                        rowClassName: ""},
        {columnName: "totalScore",      headerText: i18n.t('column_totalscore'),headerClassName: "d-none d-lg-table-cell",  rowClassName: "d-none d-lg-table-cell"},
        {columnName: "minScore",        headerText: i18n.t('column_minthrows'), headerClassName: "d-none d-sm-table-cell",  rowClassName: "d-none d-sm-table-cell"},
        {columnName: "maxScore",        headerText: i18n.t('column_maxthrows'), headerClassName: "d-none d-sm-table-cell",  rowClassName: "d-none d-sm-table-cell"},
        {columnName: "numberOfEvents",  headerText: i18n.t('column_rounds'),    headerClassName: "d-none d-lg-table-cell",  rowClassName: "d-none d-lg-table-cell"}        
      ]
    };
  }

  getDataForGrid(data)
  {
    return data;
  }

  render() {
    let contents = this.state.loading
      ? <p><em>{i18n.t('common_loading')}</em></p>
      : this.renderSeriesTable(this.state.series);
     
    return (
      <div>
        <h1 id="tabelLabel">{i18n.t('leaderboard_score_header')}</h1>
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
