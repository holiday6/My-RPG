using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace holiday6rpg
{
    public class CameraController : MonoBehaviour
    {
        // look at.
        public Transform target;

        public float translateSlerp = 1000f;    // 移動位置的平滑調整

        public float currentZoom = 5f;          // 鏡頭縮放
        public float zoomSpeed = 100f;          // 縮放速度

        public float zoomMax = 10f;             // 縮放最大值
        public float zoomMin = 2f;              // 縮放最小值

        public float rotateSpeedUD = 100f;      // 上下選轉鏡頭速度
        public float rotateSpeedLR = 100f;      // 左右旋轉鏡頭速度
        public float rotateSlerp = 50f;         // 選轉鏡頭的平滑調整

        public float udDegree = 150f;           // target至鏡頭的YZ平面夾角
        public float udDgreeMin = 100f;         // target至鏡頭的YZ平面夾角最小值
        public float udDegreeMax = 200f;        // target至鏡頭的YZ平面夾角最大值
        public float startZoomInDegree = 170f;  // 開始自動調近鏡頭距離的target至鏡頭的YZ平面夾角
        public float lrDegree = 0f;             // target至鏡頭的XZ平面夾角

        private Vector3 lookDirection;          // 鏡頭至target的向量

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            lookDirection = target.position - transform.position;
            // 按上下鍵時上下調整第三人稱視角角度
            if (Input.GetAxis("Vertical") != 0)
            {
                udDegree -= Input.GetAxis("Vertical") * rotateSpeedUD * Time.deltaTime;
                if (udDegree <= udDgreeMin) udDegree = udDgreeMin;
                else if (udDegree >= udDegreeMax) udDegree = udDegreeMax;

                // 視角超過一定角度時自動調整鏡頭距離
                if (udDegree >= startZoomInDegree)
                {
                    currentZoom -= (currentZoom - zoomMin) / (zoomMax - zoomMin);
                    LimitCurrentZoom();
                }
            }
            // 按左右鍵時左右調整第三人稱視角角度
            else if (Input.GetAxis("Horizontal") != 0)
            {
                lrDegree -= Input.GetAxis("Horizontal") * rotateSpeedLR * Time.deltaTime;
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
                LimitCurrentZoom();
            }

        }

        void LateUpdate()
        {
            Translate();
            Rotate();
        }

        // 限制縮放程度
        private void LimitCurrentZoom()
        {
            if (currentZoom >= zoomMax) currentZoom = zoomMax;
            else if (currentZoom <= zoomMin) currentZoom = zoomMin;
        }
        // public float testUD, testLR;
        // 旋轉鏡頭對向target
        private void Rotate()
        {
            // transform.rotation = Quaternion.Euler(testUD, testLR, 0);
            transform.rotation = Quaternion.Euler(180 - udDegree, lrDegree, 0);
        }
        // 移動鏡頭跟隨target
        private void Translate()
        {
            Vector3 position = target.position + new Vector3(
                currentZoom * Mathf.Cos(udDegree * Mathf.Deg2Rad) * Mathf.Sin(lrDegree * Mathf.Deg2Rad),
                currentZoom * Mathf.Sin(udDegree * Mathf.Deg2Rad),
                currentZoom * Mathf.Cos(udDegree * Mathf.Deg2Rad) * Mathf.Cos(lrDegree * Mathf.Deg2Rad));
            transform.position = Vector3.Lerp(transform.position, position, translateSlerp * Time.deltaTime);
        }

    }
}

