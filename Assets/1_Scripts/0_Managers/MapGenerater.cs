using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerater : MonoBehaviour
{
    [SerializeField] int maxRoomCount;
    [SerializeField] GameObject room;
    [SerializeField] GameObject startRoom;
    [SerializeField] GameObject endRoom;

    private void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        Vector3 nextRoomPos = Vector3.zero;
        HashSet<Vector3> createdRoomPositions = new HashSet<Vector3>();

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.right, Vector3.left };

        for (int i = 0; i < maxRoomCount; i++)
        {
            bool roomCreatable = false;

            for(int j = 0; j<4;j++) // �ֺ� üũ
            {
                if (!createdRoomPositions.Contains(nextRoomPos + directions[j] * 1.5f))
                {
                    roomCreatable = true;
                    break;
                }
            }

            if(!roomCreatable)
            {
                i--; // ���� ���� ���������Ƿ� 1ĭ �ڷ� �̵�
                nextRoomPos = createdRoomPositions.ElementAt(Random.Range(1, createdRoomPositions.Count-2));
            }

            while(roomCreatable) // �� ����
            {
                int randomDirectionIndex = Random.Range(0, 4);
                Vector3 selectedDirection = directions[randomDirectionIndex];

                Vector3 tempPos = nextRoomPos + selectedDirection * 1.5f;

                if (!createdRoomPositions.Contains(tempPos))
                {
                    if (i == maxRoomCount - 1) 
                    { 
                        Instantiate(endRoom, tempPos, Quaternion.identity);
                        break;
                    }
                    else if (i == 0)
                    {
                        Instantiate(startRoom, tempPos, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(room, tempPos, Quaternion.identity);
                    }
                    createdRoomPositions.Add(tempPos);
                    nextRoomPos = tempPos;
                    break;
                }
            }
        }
    }
}
