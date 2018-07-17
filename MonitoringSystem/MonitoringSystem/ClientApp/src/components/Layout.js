import React from 'react';
import NavMenu from './NavMenu';
import {Container} from 'reactstrap';

export default props => (
    <div>
        <div>
            <NavMenu/>
        </div>

        <Container>
            {props.children}
        </Container>
    </div>

);
