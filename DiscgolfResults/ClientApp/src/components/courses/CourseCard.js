import { CardContent, Card, Typography } from '@mui/material';
import { Box } from '@mui/system';
import { useTranslation } from 'react-i18next';
import BarChartIcon from '@mui/icons-material/BarChart';
import { Col, Container, Row } from 'react-bootstrap';

export function CourseCard(props) {
    const { i18n } = useTranslation();

    return (
        <>            
            <Box component="span">
                <Card sx={{ minWidth: 275 }}>
                    <CardContent>
                        <Typography variant="h3" component="div">
                            <BarChartIcon /> {props.course.name}
                        </Typography>
                        <Container>
                            <Row>
                                <Col xs={12} md={6} lg={4}>
                                    <Container>
                                        <Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('course_card_numberofevents')}</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.course.numberOfEvents}</Col>
                                        </Row>
                                        <Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('course_card_numberofplayedrounds')}</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.course.numberOfPlayedRounds}</Col>
                                        </Row>
                                        {/*<Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('course_card_holeinone')}</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.holeAvg.map(x => x.hios).reduce((partialSum, a) => partialSum + a, 0)}</Col>
                                        </Row>*/}
                                    </Container>
                                </Col>
                                <Col xs={12} md={6} lg={4}>
                                    <Container>
                                        <Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('course_card_avgnumplayers')}</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.course.averageNumberOfPlayers}</Col>
                                        </Row>
                                        <Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('course_card_uniquenumplayers')}</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.course.uniqueNumberOfPlayers}</Col>
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
