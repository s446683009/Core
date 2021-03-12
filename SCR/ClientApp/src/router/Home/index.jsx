import React from 'react'
import PropTypes from 'prop-types'
import { Button } from '@material-ui/core'
import {styles} from './Home.module.css';
import Header from '../../components/Header/index';
import Slider from '../../components/Slider';
function Home(props) {
    console.log(props)

    return (
        <main>
            <Header></Header>
            <Slider />
        </main>
    )
}

Home.propTypes = {

}

export default Home

