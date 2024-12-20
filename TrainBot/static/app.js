class ChatBox{
    constructor(){
        this.args = {
            openButton: document.querySelector('.chatbox__button'),
            chatBox: document.querySelector('.chatbox__support'),
            sendButton: document.querySelector('.send__button')
        }
        this.state= false;
        this.messages = [];
    }
    display(){
        const{openButton, chatBox, sendButton}= this.args;
        openButton.addEventListener('click', ()=>this.toggleState(chatBox))
        sendButton.addEventListener('click', ()=>this.onSendButton(chatBox))

        const node= chatBox.querySelector('input');
        node.addEventListener('keyup', ({key})=>{
            if(key=="Enter")
            {
                this.onSendButton(chatBox)
            }
        })
    }

    toggleState(chatbox)
    {
        this.state=!this.state;
        if(this.state){
            chatbox.classList.add('chatbox--active')
        }else{
            chatbox.classList.remove('chatbox--active')
        }
    }

    onSendButton(chatbox)
    {
        var textField = chatbox.querySelector('input');
        let text1 = textField.value;
    
        // Kiểm tra nếu ô nhập rỗng
        if (text1 === "") {
            return;
        }
        let msg1 = {
            name: "User",
            message: text1
        };
    
        // Thêm tin nhắn vào danh sách tin nhắn
        this.messages.push(msg1);
        fetch($SCRIPT_ROOT+'predict', {
            method: 'POST',
            body: JSON.stringify({ message: text1 }),
            mode:'cors', 
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(r => r.json())
        .then(r => {
            let msg2 = {
                name: "Sam",
                message: r.answer
            };
            this.messages.push(msg2);
            this.updateChatText(chatbox)
            textField.value = '';
        })
        .catch(error => {
            console.error('Error:', error);
            this.updateChatText(chatbox)
            // Xóa nội dung ô nhập
            textField.value = '';
        });
    
        
    }

    updateChatText(chatbox) {
        var html = '';
        this.messages.slice().reverse().forEach(function(item, index) {
            if (item.name === "Sam") {
                html += '<div class="messages__item messages__item--visitor">' + item.message + '</div>';
            } else {
                html += '<div class="messages__item messages__item--operator">' + item.message + '</div>';
            }
        });
    
        const chatmessage = chatbox.querySelector('.chatbox__messages');
        chatmessage.innerHTML = html;
    }
}

const chatbox = new ChatBox();
chatbox.display();