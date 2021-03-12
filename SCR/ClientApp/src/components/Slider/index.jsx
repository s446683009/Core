import React from 'react'
import PropTypes from 'prop-types'
import styles from './Slider.module.css';
import Menu from '../Menu/index';
function Slider(props) {
    return (
        <div className={styles.wrap}>
            <div className={styles.header}>
                <div className={styles.logo}></div>
                <h2 className={styles.title}>演示系统</h2>
              
            </div>
            <div className={styles.content}>
                <Menu></Menu>
            </div>
        </div>
    )
}

Slider.propTypes = {

}

export default Slider

