import { CardContent, Card, Typography } from '@mui/material';
import { Box } from '@mui/system';
import { useTranslation } from 'react-i18next';
import BarChartIcon from '@mui/icons-material/BarChart';
import { Col, Container, Row } from 'react-bootstrap';

export function PlayerStatistics(props) {
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
                                <Col xs={12} md={6} lg={6}>
                                    <Container>
                                        <Row>
                                            <Col style={{textAlign: 'right'}}>{props.player.firstAppearance.substring(0,10)}</Col>
                                            <Col>{i18n.t('player_card_firstappearance')}</Col>
                                        </Row>
                                        <Row>
                                            <Col style={{textAlign: 'right'}}>{props.player.totalRounds}</Col>
                                            <Col>{i18n.t('player_card_totalnumberevents')}</Col>
                                        </Row>
                                        <Row>
                                            <Col style={{textAlign: 'right'}}>{props.player.bestScore} </Col>
                                            <Col>{i18n.t('player_card_bestscore')}</Col>
                                        </Row>
                                        <Row>
                                            <Col style={{textAlign: 'right'}}>{props.player.avgScore} </Col>
                                            <Col>{i18n.t('player_card_avgscore')}</Col>
                                        </Row>
                                    </Container>
                                </Col>
                                <Col xs={12} md={6} lg={6}>
                                    <Container>
                                        <Row>
                                            <Col style={{textAlign: 'right'}}>{props.player.eventResults[0].hcpAfter}</Col>
                                            <Col>{i18n.t('player_card_hcp')}</Col>
                                        </Row>
                                        <Row>
                                            <Col style={{textAlign: 'right'}}>{props.player.winPercentage}%</Col>
                                            <Col>{i18n.t('player_card_winpercentage')}</Col>
                                        </Row>
                                        <Row>
                                            <Col style={{textAlign: 'right'}}>{props.player.ctpPercentage}%</Col>
                                            <Col>{i18n.t('player_card_ctppercentage')}</Col>
                                        </Row>
                                        <Row>
                                            <Col style={{textAlign: 'right'}}>{props.player.totalCtps}</Col>
                                            <Col>{i18n.t('player_card_totalctps')}</Col>
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