/**
 * DynamicScrollViewItemExample.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */
using UnityEngine.UI;


namespace Mosframe {

    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class DynamicScrollViewItemExample : UIBehaviour, IDynamicScrollViewItem
    {


        public Text title;
        public Image background;
        public UpdatePlayerInfo playerHud; 
        public PlayerList defaultPlayers;
        public PlayerList playersInPlay;
        public PlayerList playersSubselection;
        private GameManager gameManager;
        public PlayerInfo player;
        public Button button;
        public string selector;


        public void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            defaultPlayers = gameManager.playersAllDefault;
            playersInPlay = gameManager.playersInPlay;
            playerHud = FindObjectOfType<UpdatePlayerInfo>();
            button = GetComponentInChildren<Button>();
            if (gameManager == null)
            {
                Debug.Log("Error, missing gameManager");
            } else if(defaultPlayers == null) {
                Debug.Log("Error, missing defaultPlayers");

            } else if(playersInPlay == null)
            {
                Debug.Log("Error, missing playersInPlay");
            }

        }
        public void Start()
        {
            button.onClick.AddListener(SetFocusedPlayer);
        }

        public void onUpdateItem(int index) {

            switch (selector)
            {
                case "inplay":
                    if(index < playersInPlay.players.Count)
                        player = playersInPlay.players[index];
                        SetAttr(playersInPlay, index);
                    break;

                case "default":
                    if(index < defaultPlayers.players.Count)
                    player = defaultPlayers.players[index];
                    SetAttr(defaultPlayers, index);
                    break;

                default:
                    if (index < defaultPlayers.players.Count)
                    {
                        player = defaultPlayers.players[index];
                        SetAttr(defaultPlayers, index);
                    }
                    break;
            }
            
        }

        void SetAttr(PlayerList list, int index)
        {
            this.title.text = list.players[index].name;
            switch (list.players[index].position)
            {
                case 1:
                    this.button.image.color = Color.blue;
                    break;
                case 2:
                    this.button.image.color = Color.red;
                    break;
                case 3:
                    this.button.image.color = Color.yellow;
                    break;
                case 4:
                    this.button.image.color = Color.green;
                    break;
            }
        }

        public void SetFocusedPlayer()
        {
            playerHud.currentFocusedPlayer =  player;
        }

    }
}