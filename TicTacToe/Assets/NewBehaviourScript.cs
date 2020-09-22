using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private int[,] board=new int[3,3];
    private int turn=0;
    private int mode=0;
    private int initturn=0;

    void init(){
        turn =initturn;
        for(int i=0;i<3;i++){
            for(int j=0;j<3;j++){
                board[i,j]=0;
            }
        }
    }

    int check(){
        if(board[0,0]!=0&&board[0,0]==board[1,1]&&board[0,0]==board[2,2]) return board[0,0];
        if(board[2,0]!=0&&board[2,0]==board[1,1]&&board[2,0]==board[0,2]) return board[2,0];
        int cnt=0;
        for(int i=0;i<3;i++){
            if(board[i,0]!=0&&board[i,0]==board[i,1]&&board[i,0]==board[i,2]) return board[i,0];
            if(board[0,i]!=0&&board[0,i]==board[1,i]&&board[0,i]==board[2,i]) return board[0,i];
            for(int j=0;j<3;j++){
                if(board[i,j]==0) cnt++;
            }
        }
        return cnt==0? 3:0;
    }

    void PVP(){
        for(int i=0;i<3;i++){
            for(int j=0;j<3;j++){
                switch(board[i,j]){
                    case 0:
                        if(GUI.Button(new Rect(Screen.width/2-120+i*100,Screen.height/2-140+j*100,100,100)," ")&&check()==0){
                            board[i,j]=turn+1;
                            turn=1-turn;
                        }
                        break;
                    case 1:
                        GUI.Button(new Rect(Screen.width/2-120+i*100,Screen.height/2-140+j*100,100,100),"O");
                        break;
                    case 2:
                        GUI.Button(new Rect(Screen.width/2-120+i*100,Screen.height/2-140+j*100,100,100),"X");
                        break;
                }
            }
        }
    }

    void AI(){
        if(check()!=0) return;
        int cnt=0;
        int[] chose=new int[9];
        int[] prefer={0,(2<<2)+0,2,(2<<2)+2};
        for(int i=0;i<3;i++){
            for(int j=0;j<3;j++){
                if(board[i,j]==0){
                    board[i,j]=2;
                    if(check()==2){
                        return;
                    }
                    board[i,j]=1;
                    if(check()==1){
                        board[i,j]=2;
                        return;
                    }
                    board[i,j]=0;
                    chose[cnt++]=(i<<2)+j;
                }
            }
        }
        if(board[1,1]==0){
            board[1,1]=2;
            return;
        }
        for(int i=0;i<10;i++){
            int temp1=(int)Random.Range(0,4),temp2,temp;
            while((temp2=(int)Random.Range(0,4))==temp1);
            temp=prefer[temp1];
            prefer[temp1]=prefer[temp2];
            prefer[temp2]=temp;
        }
        for(int i=0;i<4;i++){
            if(board[prefer[i]>>2,prefer[i]&3]==0){
                board[prefer[i]>>2,prefer[i]&3]=2;
                return;
            }
        }
        int rd=(int)Random.Range(0,cnt);
        board[chose[rd]>>2,chose[rd]&3]=2;
        return;
    }

    void PVA(){
        for(int i=0;i<3;i++){
            for(int j=0;j<3;j++){
                switch(board[i,j]){
                    case 0:
                        if(turn==0){
                            if(GUI.Button(new Rect(Screen.width/2-120+i*100,Screen.height/2-140+j*100,100,100)," ")&&check()==0){
                                board[i,j]=turn+1;
                                turn=1-turn;
                            }    
                        }
                        else{
                            GUI.Button(new Rect(Screen.width/2-120+i*100,Screen.height/2-140+j*100,100,100)," ");
                            AI();
                            if(check()==0||check()==2) turn=1-turn;
                        }
                        break;
                    case 1:
                        GUI.Button(new Rect(Screen.width/2-120+i*100,Screen.height/2-140+j*100,100,100),"O");
                        break;
                    case 2:
                        GUI.Button(new Rect(Screen.width/2-120+i*100,Screen.height/2-140+j*100,100,100),"X");
                        break;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI (){
        GUI.Box(new Rect(Screen.width/2-300,Screen.height/2-200,600,400), "TicTacToe");
        int state=check();
        switch(state){
            case 0:
                GUI.Box(new Rect(Screen.width/2-70,Screen.height/2-170,200,25),"进行中, 玩家"+(turn+1)+"执棋");
                break;
            case 1:
            case 2:
                GUI.Box(new Rect(Screen.width/2-70,Screen.height/2-170,200,25),"玩家"+(2-turn)+"获胜");
                break;
            case 3:
                GUI.Box(new Rect(Screen.width/2-70,Screen.height/2-170,200,25),"平局");
                break;
        }
        if(GUI.Button(new Rect(Screen.width/2-280,Screen.height/2,100,25),"重置")) init();
        if(GUI.Button(new Rect(Screen.width/2-280,Screen.height/2-120,100,25),"玩家"+(initturn+1)+"先手")){
            initturn=1-initturn;
            init();
        }
        string temp;
        if(mode==0){
            temp="玩家";
        }
        else{
            temp="AI";
        }
        if(GUI.Button(new Rect(Screen.width/2-280,Screen.height/2-90,100,25),"玩家2: "+temp)){
            mode=1-mode;
            init();
        }
        if(mode==0){
            PVP();
        }
        else{
            PVA();
        }
    }
}
