import { CardContent, Card, Typography } from '@mui/material';
import { Box } from '@mui/system';
import { useTranslation } from 'react-i18next';
import BarChartIcon from '@mui/icons-material/BarChart';
import { Col, Container, Row } from 'react-bootstrap';

export function SummaryCard(props) {
    const { i18n } = useTranslation();

    return (
        <>            
            <Box component="span">
                <Card sx={{ minWidth: 275 }}>
                    <CardContent>
                        <Typography variant="h3" component="div">
                            <BarChartIcon /> {props.player.fullName}
                        </Typography>
                        <Container>
                            <Row>
                                <Col xs={12} md={6} lg={4}>
                                    <Container>
                                        <Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('player_card_firstappearance')}</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.player.firstAppearance.substring(0,10)}</Col>
                                        </Row>
                                        <Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('player_card_totalnumberevents')}</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.player.totalRounds}</Col>
                                        </Row>
                                        <Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('player_card_bestscore')}</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.player.bestScore} </Col>
                                        </Row>
                                        <Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('player_card_avgscore')}</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.player.avgScore} </Col>
                                        </Row>
                                        <Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('player_card_stddev')} (&sigma;)</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.player.stdDeviation}</Col>
                                            
                                        </Row>
                                    </Container>
                                </Col>
                                <Col xs={12} md={6} lg={4}>
                                    <Container>
                                        <Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('player_card_ctppercentage')}</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.player.ctpPercentage}%</Col>
                                        </Row>
                                        <Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('player_card_totalctps')}</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.player.totalCtps}</Col>
                                        </Row>
                                        <Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('player_card_hcp')}</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.player.eventResults[0].hcpAfter}</Col>
                                        </Row>
                                        <Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('player_card_winpercentagehcp')}</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.player.winPercentageHcp}%</Col>
                                        </Row>                                        
                                        <Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('player_card_winpercentage')}</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.player.winPercentage}%</Col>
                                        </Row>
                                    </Container>
                                </Col>
                            </Row>
                        </Container>
                    </CardContent>
                </Card>
            </Box>         
        </>
    ); 
}