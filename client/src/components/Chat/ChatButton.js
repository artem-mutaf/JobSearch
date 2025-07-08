import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import styles from './ChatButton.module.scss';
import ChatWindow from './ChatWindow'

function ChatButton() {
    const[visible, setVisible] = useState(false);
    const[open, setOpen] = useState(false);
    const [isClosing, setIsClosing] = useState(false);
    
    useEffect(() => {
        const timer = setTimeout(() => {
            setVisible(true);
        }, 2000);

        return () => clearTimeout(timer);
    }, [])

    const toggleChat = () => setOpen(prev => !prev)

    const handleClose = () => {
        setIsClosing(true);
        setTimeout(() => {
            setOpen(false);
            setIsClosing(false);
        }, 1000);
    };

    return (
        <div className={styles.wrapper}>
            {!open && (
                <button
                    className={`${styles.chatButton} ${visible ? styles.show : ''}`}
                    onClick={() => setOpen(true)}
                >
                    💬
                </button>
            )}
            {open && (
                <ChatWindow
                    onClose={handleClose}
                    className={`${isClosing ? 'closing' : ''}`}
                />
            )}
        </div>
    )
}

export default ChatButton;