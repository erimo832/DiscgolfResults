import Collapse, { Panel } from 'rc-collapse';
import { Results } from './Results';

export function DivisionResults(props) {
    let content =  props.result.map(x => <Panel header={`${x.name}`} key={x.name}><Results results={x.results}></Results></Panel>);

    return (
        <>            
            <Collapse accordion={false}>
                {content}
            </Collapse>         
        </>
    ); 
}